
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using IT.DigitalCompany.Data;
using IT.DigitalCompany.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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

        internal DbSet<Address> Addresses => Context.Set<Address>();
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
            var address = person.Address;
            if(null != address)
            {
                this.Addresses.Add(address);
            }
            (companyRegistrationRequest.Associates ??= new List<Person>())
                    .Add(person);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        public async Task UpdateRegistrationRequestAssociateAsync(Person person)
        {
            if (null == person) throw new ArgumentNullException(nameof(person));

            var address = person.Address;
            AddOrUpdateAddress(address);
            this.Persons.Update(person);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        public async Task DeleteRegistrationRequestAssociateAsync(Guid id)
        {

            var p = await this.Persons
                .Include(p => p.Address)
                .SingleAsync(p => p.Id.Equals(id));

            RemovePerson(p);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        private void RemovePerson(Person? p)
        {
            if (null != p)
            {
                var address = p.Address;
                if (null != address)
                {
                    this.Addresses.Remove(address);
                }
                this.Persons.Remove(p);
            }

        }
        public async Task AddRegistrationRequestLocationAsync(CompanyRegistrationRequest companyRegistrationRequest, CompanyLocation companyLocation)
        {
            if(null == companyRegistrationRequest) throw new ArgumentNullException(nameof(companyRegistrationRequest));
            if(null == companyLocation) throw new ArgumentNullException(nameof(companyLocation));

            Context.CompanyRegistrationRequests.Attach(companyRegistrationRequest);
            var address = companyLocation.Address;
            AddOrUpdateAddress(address);
            if(null != address)
            {
                Addresses.Add(address);
            }
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

        private void AddOrUpdateAddress(Address? address)
        {
            if (null != address)
            {
                Addresses.Update(address);
                /*
                if (address.IsNew)
                {
                    Addresses.Add(address);
                }
                else
                {
                    Addresses.Update(address);
                }*/
            }

        }
        public async Task<CompanyLocation> UpdateRegistrationRequestLocationAsync(CompanyLocation companyLocation)
        {
            
            if (null == companyLocation) throw new ArgumentNullException(nameof(companyLocation));
            var dbLocation = Locations
                .Include(l => l.Address)
                .Include(l => l.Owners)
                .ThenInclude(o => o.Address)
                .FirstOrDefault(l => l.Id.Equals(companyLocation.Id));

            if (null == dbLocation)
                throw new KeyNotFoundException();

            Locations.Entry(dbLocation).CurrentValues.SetValues(companyLocation);
            dbLocation.Contract = companyLocation.Contract;

    
            var dbAddress = dbLocation.Address?? new Address();
            var newAddress = companyLocation.Address;
            if (dbAddress != null)
            {
                Addresses.Entry(dbAddress).CurrentValues.SetValues(newAddress);
              
            }
            else
            {
                Addresses.Add(newAddress);
                dbLocation.Address = newAddress;
            }
            var newOwners = companyLocation.Owners??Enumerable.Empty<Person>();
            var dbOwners = dbLocation.Owners ??= new Collection<Person>();
            foreach(var newOwner in newOwners)
            {
                var dbOwner = dbOwners.FirstOrDefault(o => o.Id.Equals(newOwner.Id));
                if(null == dbOwner)
                {
                    dbOwners.Add(newOwner);
                }
                else
                {
                    Persons.Entry(dbOwner).CurrentValues.SetValues(newOwner);
                    dbOwner.Contact = newOwner.Contact;
                    dbOwner.IdentityDocument = newOwner.IdentityDocument;

                }
                var dbOwnerAddress = dbOwner?.Address;
                var newOwnerAddress = newOwner.Address;
                if(null != dbOwnerAddress)
                {
                    if (null != newOwnerAddress)
                    {
                        Addresses.Entry(dbOwnerAddress).CurrentValues.SetValues(newOwnerAddress);
                    }
                    else
                    {
                        Addresses.Remove(dbOwnerAddress);
                        if (null != dbOwner)
                        {
                            dbOwner.Address = newOwnerAddress;
                        }
                    }
                }
                else if (null != dbOwner)
                {
                    dbOwner.Address = newOwnerAddress;
                }

            }

            foreach (var dbOwner in dbOwners)
            {
                if(!newOwners.Any(o => o.Id.Equals(dbOwner.Id)))
                {
                    Persons.Remove(dbOwner);
                }
            }
            await Context.SaveChangesAsync()
                .ConfigureAwait(false);

            return dbLocation;
        }

        public async Task<CompanyRegistrationRequest?> FindCompanyRegistrationRequestAsync(Guid id
                        , Boolean includeAssociates = false
                        , Boolean includeAssociateAddress = false
                        , Boolean includeLocations = false
                        , Boolean includeLocationAddress = false 
                        , Boolean includeLocationOwners = false
                        , Boolean includeLocationOwnerAddress = false)
        {
            IQueryable<CompanyRegistrationRequest> query = this.Context.CompanyRegistrationRequests;
            if (includeAssociates)
            {
                var t = query.Include(r => r.Associates);
                query = t;
                if (includeAssociateAddress)
                {
                    query = t.ThenInclude(a => a.Address);
                }
               
            }
            if (includeLocations)
            {
                var t = query.Include(r => r.Locations);
                query = t;
                if (includeLocationAddress)
                {
                    query = t.ThenInclude(l => l.Address);
                }
                if (includeLocationOwners)
                {
                   
                    var t1 = query.Include(r => r.Locations).ThenInclude(l => l.Owners);
                    query = t1;
                    if(includeLocationOwnerAddress)
                    {
                        query = t1.ThenInclude(t1 => t1.Address);
                    }
                }
            }


            var r = await query
                .SingleAsync(r => r.Id.Equals(id))
                .ConfigureAwait(false);

            return r;
        }

        public async Task<CompanyRegistrationRequest?> FindCompanyRegistrationRequestAsync(Expression<Func<CompanyRegistrationRequest, Boolean>> predicate
                        , Boolean includeAssociates = false
                        , Boolean includeAssociateAddress = false
                        , Boolean includeLocations = false
                        , Boolean includeLocationAddress = false
                        , Boolean includeLocationsOwners = false
                        , Boolean includeLocationOwnerAddress = false)
        {
            IQueryable<CompanyRegistrationRequest> query =  this.Context.CompanyRegistrationRequests;
            if (includeAssociates)
            {
                var t = query.Include(r => r.Associates);
                query = t;
                if (includeAssociateAddress)
                {
                    query = t.ThenInclude(a => a.Address);
                }

            }
            if (includeLocations)
            {
                var t = query.Include(r => r.Locations);
                query = t;
                if (includeLocationAddress)
                {
                    query = t.ThenInclude(l => l.Address);
                }
                if (includeLocationsOwners)
                {

                    var t1 = query.Include(r => r.Locations).ThenInclude(l => l.Owners);
                    query = t1;
                    if (includeLocationOwnerAddress)
                    {
                        query = t1.ThenInclude(t1 => t1.Address);
                    }
                }
            }


            var r = await query.SingleAsync(predicate, CancellationToken.None)
                .ConfigureAwait(false);

            return r;
        }

    }


}
