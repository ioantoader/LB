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
            var a = OnAddressModelCreating(modelBuilder);
            var p = OnPersonModelCreating(modelBuilder);
            OnCompanyLocationModelCreating(modelBuilder);
            var cr = OnCompanyRegistrationRequestsModelCreating(modelBuilder);
                                        
        }

        protected EntityTypeBuilder<CompanyRegistrationRequest> OnCompanyRegistrationRequestsModelCreating(ModelBuilder modelBuilder)
        {
            var companyRequestEntity = modelBuilder.Entity<CompanyRegistrationRequest>()
                .ToTable("CompanyRegistrationRequests");
            companyRequestEntity.Property(p => p.Id).IsRequired();
            companyRequestEntity.Property(p => p.UserId).IsRequired();

            var contactEntity = companyRequestEntity.OwnsOne(p => p.Contact);

            OnContactModelCreating(modelBuilder, contactEntity,
                firstNameRequiered: false, lastNameRequiered: false);

            companyRequestEntity.HasMany(e => e.Associates)
                .WithMany()
                .UsingEntity<CompanyRegistrationRequestAssociates>(joinEntityName: nameof(CompanyRegistrationRequestAssociates),
                configureRight: r => r.HasOne<Person>().WithMany().HasForeignKey(e => e.AssociateId).HasPrincipalKey(e => e.Id),
                configureLeft: l => l.HasOne<CompanyRegistrationRequest>().WithMany().HasForeignKey(e => e.CompanyRegistationRequestId).HasPrincipalKey(e => e.Id),
                configureJoinEntityType: j => j.HasKey(e => new { e.CompanyRegistationRequestId, e.AssociateId })
            );
            companyRequestEntity.HasMany(e => e.Locations)
                .WithMany()
                .UsingEntity<CompanyRegistrationRequestLocations>(joinEntityName: nameof(CompanyRegistrationRequestLocations),
                configureRight: r => r.HasOne<CompanyLocation>().WithMany().HasForeignKey(e => e.LocationId).HasPrincipalKey(e => e.Id),
                configureLeft: l => l.HasOne<CompanyRegistrationRequest>().WithMany().HasForeignKey(e => e.CompanyRegistationRequestId).HasPrincipalKey(e => e.Id),
                configureJoinEntityType: j => j.HasKey(e => new { e.CompanyRegistationRequestId, e.LocationId })
                );

            var companyNmaesEntity = companyRequestEntity.OwnsOne(e => e.Names);
            companyNmaesEntity.Property(p => p.Name1).
                HasColumnName($"Names_{nameof(CompanyNames.Name1)}")
                .IsRequired(false);
            companyNmaesEntity.Property(p => p.Name2).
                HasColumnName($"Names_{nameof(CompanyNames.Name2)}")
                .IsRequired(false);
            companyNmaesEntity.Property(p => p.Name3).
                HasColumnName($"Names_{nameof(CompanyNames.Name3)}")
                .IsRequired(false);

            companyRequestEntity.HasKey(p => p.Id);
            return companyRequestEntity;

        }

        protected void OnContactModelCreating<TOwner>(ModelBuilder modelBuilder, OwnedNavigationBuilder<TOwner, Contact> contactEntity,
            Boolean firstNameRequiered, Boolean lastNameRequiered)
            where TOwner : class
        {
            const string prefix = nameof(Contact);
            contactEntity.Property(p => p.FirstName)
                .HasColumnName($"{prefix}_{nameof(Contact.FirstName)}")
                .IsRequired(firstNameRequiered);

            contactEntity.Property(p => p.LastName)
                .HasColumnName($"{prefix}_{nameof(Contact.LastName)}")
                .IsRequired(lastNameRequiered);

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

        protected EntityTypeBuilder<Address> OnAddressModelCreating(ModelBuilder modelBuilder)
        {
            const String? prefix = null; //nameof(Address);
            var addressEntity = modelBuilder.Entity<Address>()
                .ToTable("Address");
            addressEntity.Property(p => p.Id)
                .IsRequired(true);

            addressEntity.Property(p => p.Country)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Country)))
                .IsRequired();

            addressEntity.Property(p => p.City)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.City)))
                .IsRequired();

            addressEntity.Property(p => p.PostalCode)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.PostalCode)))
                .IsRequired();

            addressEntity.Property(p => p.Number)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Number)))
                .IsRequired();

            addressEntity.Property(p => p.Street)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Street)))
                .IsRequired();

            addressEntity.Property(p => p.State)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.State)))
                .IsRequired(false);


            addressEntity.Property(p => p.Block)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Block)))
                .IsRequired(false);

            addressEntity.Property(p => p.Stair)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Stair)))
                .IsRequired(false);

            addressEntity.Property(p => p.Floor)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Floor)))
                .IsRequired(false);

            addressEntity.Property(p => p.Apartment)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Apartment)))
                .IsRequired(false);

            addressEntity.HasKey(p => p.Id);

            return addressEntity;
        }

        /*
        protected void OnAddressModelCreating<TOwner>(ModelBuilder modelBuilder, OwnedNavigationBuilder<TOwner, Address> addressEntity)
            where TOwner : class
        {
            const String? prefix = null; //nameof(Address);

            addressEntity.Property(p => p.Id)
                .IsRequired(true);

            addressEntity.Property(p => p.Country)
                .HasColumnName(ComputeColumnName(prefix,nameof(Address.Country)))
                .IsRequired();

            addressEntity.Property(p => p.City)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.City)))
                .IsRequired();

            addressEntity.Property(p => p.PostalCode)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.PostalCode)))
                .IsRequired();

            addressEntity.Property(p => p.Number)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Number)))
                .IsRequired();

            addressEntity.Property(p => p.Street)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Street)))
                .IsRequired();

            addressEntity.Property(p => p.State)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.State)))
                .IsRequired(false);


            addressEntity.Property(p => p.Block)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Block)))
                .IsRequired(false);

            addressEntity.Property(p => p.Stair)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Stair)))
                .IsRequired(false);

            addressEntity.Property(p => p.Floor)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Floor)))
                .IsRequired(false);

            addressEntity.Property(p => p.Apartment)
                .HasColumnName(ComputeColumnName(prefix, nameof(Address.Apartment)))
                .IsRequired(false);

            addressEntity.HasKey(p => p.Id);

        }
        */
        protected void OnCompanyLocationContractModelCreating<TOwner>(ModelBuilder modelBuilder, OwnedNavigationBuilder<TOwner, CompanyLocationContract> companyLocationContractEntity)
            where TOwner : class
        {
            const string prefix = nameof(CompanyLocationContract);
            companyLocationContractEntity.Property(p => p.DurationInYears)
                .HasColumnName($"{prefix}_{nameof(CompanyLocationContract.DurationInYears)}")
                .IsRequired();

            companyLocationContractEntity.Property(p => p.MonthlyRental)
                .HasColumnName($"{prefix}_{nameof(CompanyLocationContract.MonthlyRental)}")
                .HasPrecision(18,2)
                .IsRequired(false);

            companyLocationContractEntity.Property(p => p.RentalDeposit)
                .HasColumnName($"{prefix}_{nameof(CompanyLocationContract.RentalDeposit)}")
                .HasPrecision(18,2)
                .IsRequired(false);

        }

        protected EntityTypeBuilder<Person> OnPersonModelCreating(ModelBuilder modelBuilder)
        {
            var personEntity = modelBuilder.Entity<Person>()
                .ToTable("Persons");                

            personEntity.Property(p => p.Id)
                .IsRequired();
            personEntity.Property(p => p.Type);
            personEntity.Property(p => p.CRN)
                .IsRequired(false);

            var contactEntity = personEntity.OwnsOne(p => p.Contact);
            OnContactModelCreating(modelBuilder, contactEntity,
                firstNameRequiered: true, lastNameRequiered: true);

            var identityDocumentEntity = personEntity.OwnsOne(p => p.IdentityDocument);
            OnIdentityDocumentModelCreating(modelBuilder, identityDocumentEntity);
            personEntity.Property<Guid?>("AddressId");
            personEntity.HasOne(p => p.Address)
                .WithOne()
                .HasForeignKey<Person>("AddressId")
                .OnDelete(DeleteBehavior.Cascade);
                /*, builder =>
            {
                builder.ToTable("Addresses");
                builder.WithOwner().HasForeignKey("AddressId");
                OnAddressModelCreating(modelBuilder, builder);
            });*/
            

            personEntity.HasKey(p => p.Id);
            return personEntity;
        }

        protected EntityTypeBuilder<CompanyLocation> OnCompanyLocationModelCreating(ModelBuilder modelBuilder)
        {
            var companyLocationEntity = modelBuilder.Entity<CompanyLocation>()
                .ToTable("CompanyLocations");
            companyLocationEntity.Property(p => p.Id)
                .IsRequired();

            companyLocationEntity.Property<Guid>("AddressId");
            companyLocationEntity.HasOne(p => p.Address)
                .WithOne()
                .HasForeignKey<CompanyLocation>("AddressId")
                .OnDelete(DeleteBehavior.NoAction);
                //.OnDelete(DeleteBehavior.NoAction);
            
            /*, builder =>
            {
                builder.ToTable("Addresses");
                builder.WithOwner().HasForeignKey("AddressId");
                OnAddressModelCreating(modelBuilder, builder);
            });*/

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

        private String ComputeColumnName(String? prefix, String columnName)
        {
            return String.IsNullOrWhiteSpace(prefix) ? columnName : $"{prefix}_{columnName}";
        }
    }
}
