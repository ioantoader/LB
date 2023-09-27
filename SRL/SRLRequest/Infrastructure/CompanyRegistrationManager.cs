
using System.Linq;
using System.Linq.Expressions;
using IT.DigitalCompany.Data;
using IT.DigitalCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace IT.DigitalCompany.Infrastructure
{
    public class CompanyRegistrationManager
    {
        public CompanyRegistrationManager(CompanyRegistrationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        CompanyRegistrationDbContext Context {  get; set; }

        internal DbSet<CompanyLocation> Locations => Context.Set<CompanyLocation>();
        internal DbSet<Person> Persons => Context.Set<Person>();

        internal DbSet<CompanyRegistrationRequest> CompanyRegistrationRequests => Context.CompanyRegistrationRequests;
        public async Task CreateRegistrationRequestAsync(CompanyRegistrationRequest companyRequest)
        {
            if (null == companyRequest) throw new ArgumentNullException(nameof(companyRequest));

            this.Context.CompanyRegistrationRequests.Add(companyRequest);

            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public async Task UpdateRegistrationRequestContactAsync(CompanyRegistrationRequest companyRegistrationRequest, Contact contact)
        {
            if (null == companyRegistrationRequest) throw new ArgumentNullException(nameof(companyRegistrationRequest));
            if (null == contact) throw new ArgumentNullException(nameof(contact));
            
            this.Context.CompanyRegistrationRequests.Attach(companyRegistrationRequest);
            var c = companyRegistrationRequest.Contact ??= new Contact();
            c.FirstName = contact?.FirstName;
            c.LastName = contact?.LastName;
            c.Email = contact?.Email;
            c.PhoneNumber = contact?.PhoneNumber;
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);
  
        }
                       

        public async Task AddRegistrationRequestAssociateAsync(CompanyRegistrationRequest companyRegistrationRequest, Person person)
        {
            if (null == companyRegistrationRequest) throw new ArgumentNullException(nameof(companyRegistrationRequest));
            if (null == person) throw new ArgumentNullException(nameof(person));


            this.Context.CompanyRegistrationRequests.Attach(companyRegistrationRequest);
            this.Persons.Add(person);
            (companyRegistrationRequest.Associates ??= new List<Person>())
                    .Add(person);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        public async Task UpdateRegistrationRequestAssociateAsync(Person person)
        {
            if (null == person) throw new ArgumentNullException(nameof(person));

            this.Persons.Attach(person);
            this.Persons.Update(person);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        public async Task DeleteRegistrationRequestAssociateAsync(Guid id)
        {

            var p = this.Persons.Local.FirstOrDefault(p => p.Id.Equals(id))??new Person()
            {
                Id = id
            };

            this.Persons.Remove(p);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }
        public async Task AddRegistrationRequestLocationAsync(CompanyRegistrationRequest companyRegistrationRequest, CompanyLocation companyLocation)
        {
            if(null == companyRegistrationRequest) throw new ArgumentNullException(nameof(companyRegistrationRequest));
            if(null == companyLocation) throw new ArgumentNullException(nameof(companyLocation));

            Context.CompanyRegistrationRequests.Attach(companyRegistrationRequest);
            Locations.Add(companyLocation);
            var owners = companyLocation.Owners?? Enumerable.Empty<Person>();
            foreach (var owner in owners)
            {
                this.Persons.Add(owner);
            }
            companyRegistrationRequest.Locations.Add(companyLocation);
            
            await Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }
        public async Task UpdateRegistrationRequestLocationAsync(CompanyLocation companyLocation)
        {
            
            if (null == companyLocation) throw new ArgumentNullException(nameof(companyLocation));

            var dbLocation = await this.Locations
                .AsNoTracking()
                .Include(l => l.Owners)
                .AsNoTracking()
                .SingleAsync(c => c.Id.Equals(companyLocation.Id))
                .ConfigureAwait(false);

            if(null == dbLocation)
                throw new KeyNotFoundException(companyLocation.Id.ToString());
            
            Locations.Attach(companyLocation);
            var dbOwners = dbLocation.Owners?? Enumerable.Empty<Person>();
            var newOwners = companyLocation.Owners ?? Enumerable.Empty<Person>();
            foreach(var dbOwner in dbOwners)
            {
                if(!newOwners.Any(no => no.Id.Equals(dbOwner.Id)))
                {
                    Persons.Remove(dbOwner);
                } 
            }
            foreach (var owner in newOwners)
            {
                if (owner.IsNew)
                {
                    this.Persons.Add(owner);
                }
                else
                {
                    this.Persons.Attach(owner);
                    this.Persons.Update(owner);
                }
            }
            
            Locations.Update(companyLocation);
            await Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        public async Task<CompanyRegistrationRequest?> FindCompanyRegistrationRequestAsync(Guid id
                        , Boolean includeAssociates = false
                        , Boolean includeLocations = false
                        , Boolean includeLocationsOwners = false)
        {
            IQueryable<CompanyRegistrationRequest> query = this.Context.CompanyRegistrationRequests;
            if (includeAssociates)
            {
                query = query.Include(r => r.Associates);
            }
            if (includeLocations)
            {
                var t = query.Include(r => r.Locations);
                query = t;
                if (includeLocationsOwners)
                {
                    query = t.ThenInclude(l => l.Owners);
                }
            }


            var r = await query
                .SingleAsync(r => r.Id.Equals(id))
                .ConfigureAwait(false);

            return r;
        }

        public async Task<CompanyRegistrationRequest?> FindCompanyRegistrationRequestAsync(Expression<Func<CompanyRegistrationRequest, Boolean>> predicate
            ,Boolean includeAssociates = false
            ,Boolean includeLocations = false
            ,Boolean includeLocationsOwners = false)
        {
            IQueryable<CompanyRegistrationRequest> query =  this.Context.CompanyRegistrationRequests;
            if(includeAssociates)
            {
                query = query.Include(r => r.Associates);
            }
            if(includeLocations)
            {
                var t = query.Include(r => r.Locations);
                query = t;
                if(includeLocationsOwners)
                {
                    query = t.ThenInclude(l => l.Owners);
                }
            }

            var r = await query.SingleAsync(predicate, CancellationToken.None)
                .ConfigureAwait(false);

            return r;
        }

    }


}
