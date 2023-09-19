using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using IT.DigitalCompany.Models;

namespace IT.DigitalCompany.Data
{
    public class CompanyRegistrationDbContext : DbContext
    {
        public CompanyRegistrationDbContext(DbContextOptions<CompanyRegistrationDbContext> options) 
            :base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var p = OnPersonModelCreating(modelBuilder);
            var cr = OnCompanyRegistrationRequestsModelCreating(modelBuilder);
            cr.HasMany(e => e.Associates)
                .WithMany()
                .UsingEntity< CompanyRequestAssociates>(joinEntityName: nameof(CompanyRequestAssociates),                
                configureRight: r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.AssociateId).HasPrincipalKey(e => e.Id),
                configureLeft: l => l.HasOne<CompanyRegistrationRequest>().WithMany().HasForeignKey(e => e.CompanyRequestId).HasPrincipalKey(e => e.Id),
                configureJoinEntityType: j => j.HasKey(e => new { e.CompanyRequestId, e.AssociateId })
                );
                                        
        }

        protected EntityTypeBuilder<CompanyRegistrationRequest> OnCompanyRegistrationRequestsModelCreating(ModelBuilder modelBuilder)
        {
            var companyRequestEntity = modelBuilder.Entity<CompanyRegistrationRequest>();
            companyRequestEntity.Property(p => p.Id).IsRequired();
            companyRequestEntity.Property(p => p.UserId).IsRequired();

            var contactEntity = companyRequestEntity.OwnsOne(p => p.Contact);

            OnContactModelCreating(modelBuilder, contactEntity);

            OnCompanyLocationModelCreating(modelBuilder);

            companyRequestEntity.HasKey(p => p.Id);
            return companyRequestEntity;

        }

        protected void OnContactModelCreating<TOwner>(ModelBuilder modelBuilder, OwnedNavigationBuilder<TOwner, Contact> contactEntity)
            where TOwner : class
        {
            const string prefix = nameof(Contact);
            contactEntity.Property(p => p.FirstName)
                .HasColumnName($"{prefix}_{nameof(Contact.FirstName)}");

            contactEntity.Property(p => p.LastName)
                .HasColumnName($"{prefix}_{nameof(Contact.LastName)}");

            contactEntity.Property(p => p.PhoneNumber)
                .HasColumnName($"{prefix}_{nameof(Contact.PhoneNumber)}");

            contactEntity.Property(p => p.Email)
                .HasColumnName($"{prefix}_{nameof(Contact.Email)}");

        }

        protected void OnIdentityDocumentModelCreating<TOwner>(ModelBuilder modelBuilder, OwnedNavigationBuilder<TOwner, IdentityDocument> identityDocumentEntity)
            where TOwner : class
        {
            const string prefix = "ID";
            identityDocumentEntity.Property(p => p.DocumentType)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.DocumentType)}");

            identityDocumentEntity.Property(p => p.Serial)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.Serial)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.Number)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.Number)}");

            identityDocumentEntity.Property(p => p.CNP)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.CNP)}");

            identityDocumentEntity.Property(p => p.BirthCountry)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.BirthCountry)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.BirthState)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.BirthState)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.BirthCity)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.BirthCity)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.Citizenship)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.Citizenship)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.IssueDate)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.IssueDate)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.ExpirationDate)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.ExpirationDate)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.IssueBy)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.IssueBy)}")
                .IsRequired(false);

            identityDocumentEntity.Property(p => p.BirthDate)
                .HasColumnName($"{prefix}_{nameof(IdentityDocument.BirthDate)}")
                .IsRequired(false);

        }

        protected void OnAddressModelCreating<TOwner>(ModelBuilder modelBuilder, OwnedNavigationBuilder<TOwner, Address> addressEntity)
            where TOwner : class
        {
            const string prefix = nameof(Address);
            addressEntity.Property(p => p.Country)
                .HasColumnName($"{prefix}_{nameof(Address.Country)}")
                .IsRequired();

            addressEntity.Property(p => p.City)
                .HasColumnName($"{prefix}_{nameof(Address.City)}")
                .IsRequired();

            addressEntity.Property(p => p.PostalCode)
                .HasColumnName($"{prefix}_{nameof(Address.PostalCode)}")
                .IsRequired();

            addressEntity.Property(p => p.Number)
                .HasColumnName($"{prefix}_{nameof(Address.Number)}")
                .IsRequired();

            addressEntity.Property(p => p.Street)
                .HasColumnName($"{prefix}_{nameof(Address.Street)}")
                .IsRequired();

            addressEntity.Property(p => p.State)
                .HasColumnName($"{prefix}_{nameof(Address.State)}")
                .IsRequired(false);

            addressEntity.Property(p => p.Block)
                .HasColumnName($"{prefix}_{nameof(Address.Block)}")
                .IsRequired(false);

            addressEntity.Property(p => p.Stair)
                .HasColumnName($"{prefix}_{nameof(Address.Stair)}")
                .IsRequired(false);

            addressEntity.Property(p => p.Floor)
                .HasColumnName($"{prefix}_{nameof(Address.Floor)}")
                .IsRequired(false);

            addressEntity.Property(p => p.Apartment)
                .HasColumnName($"{prefix}_{nameof(Address.Apartment)}")
                .IsRequired(false);

        }

        protected void OnCompanyLocationContractModelCreating<TOwner>(ModelBuilder modelBuilder, OwnedNavigationBuilder<TOwner, CompanyLocationContract> companyLocationContractEntity)
            where TOwner : class
        {
            const string prefix = nameof(CompanyLocationContract);
            companyLocationContractEntity.Property(p => p.DurationInYears)
                .HasColumnName($"{prefix}_{nameof(CompanyLocationContract.DurationInYears)}")
                .IsRequired();

            companyLocationContractEntity.Property(p => p.MonthlyRental)
                .HasColumnName($"{prefix}_{nameof(CompanyLocationContract.MonthlyRental)}")
                .IsRequired(false);

            companyLocationContractEntity.Property(p => p.RentalDeposit)
                .HasColumnName($"{prefix}_{nameof(CompanyLocationContract.RentalDeposit)}")
                .IsRequired(false);

        }

        protected EntityTypeBuilder<Person> OnPersonModelCreating(ModelBuilder modelBuilder)
        {
            var associateEntity = modelBuilder.Entity<Person>();                

            associateEntity.Property(p => p.Id)
                .IsRequired();
            associateEntity.Property(p => p.Type);
            associateEntity.Property(p => p.CRN)
                .IsRequired(false);

            var contactEntity = associateEntity.OwnsOne(p => p.Contact);
            OnContactModelCreating(modelBuilder, contactEntity);

            var identityDocumentEntity = associateEntity.OwnsOne(p => p.IdentityDocument);
            OnIdentityDocumentModelCreating(modelBuilder, identityDocumentEntity);
            
            var addressEntity = associateEntity.OwnsOne(p => p.Address);
            OnAddressModelCreating(modelBuilder, addressEntity);

            associateEntity.HasKey(p => p.Id);
            return associateEntity;
        }

        protected EntityTypeBuilder<CompanyLocation> OnCompanyLocationModelCreating(ModelBuilder modelBuilder)
        {
            var companyLocationEntity = modelBuilder.Entity<CompanyLocation>();
            companyLocationEntity.Property(p => p.Id)
                .IsRequired();

            var addressEntity = companyLocationEntity.OwnsOne(p => p.Address);
            OnAddressModelCreating(modelBuilder, addressEntity);

            var contractEntity = companyLocationEntity.OwnsOne(p => p.Contract);
            OnCompanyLocationContractModelCreating(modelBuilder, contractEntity);

            companyLocationEntity.HasMany(p => p.Owners)
                .WithMany()
                .UsingEntity<CompanyLocationOwners>(joinEntityName: nameof(CompanyLocationOwners),
                configureRight: r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.OwnerId).HasPrincipalKey(e => e.Id),
                configureLeft: l => l.HasOne<CompanyLocation>().WithMany().HasForeignKey(e => e.CompanyLocationId).HasPrincipalKey(e => e.Id),
                configureJoinEntityType: j => j.HasKey(e => new { e.CompanyLocationId, e.OwnerId }));
            companyLocationEntity.HasKey(p => p.Id);

            return companyLocationEntity;
        }
        public DbSet<CompanyRegistrationRequest> CompanyRegistrationRequests { get; set; }

    }
}
