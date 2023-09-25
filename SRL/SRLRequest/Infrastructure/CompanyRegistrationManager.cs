
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


            this.Context.Attach(companyRegistrationRequest);
            if (person.Id.Equals(Guid.Empty))
            {
                this.Context.Attach(person);
            }
            else
            {
                this.Context.Add(person);

            }
            (companyRegistrationRequest.Associates ??= new List<Person>())
                    .Add(person);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        public async Task UpdateRegistrationRequestAssociateAsync(Person person)
        {
            if (null == person) throw new ArgumentNullException(nameof(person));

            this.Context.Attach(person);
            this.Context.Update(person);
            await this.Context.SaveChangesAsync()
                .ConfigureAwait(false);

        }

        public async Task<CompanyRegistrationRequest?> FindCompanyRegistrationRequestAsync(Guid id)
        {
            var r = await this.Context.CompanyRegistrationRequests
                .Include(r => r.Associates)
                .Include(r => r.Locations)
                .ThenInclude(l => l.Owners)
                .SingleAsync(r => r.Id.Equals(id))
                .ConfigureAwait(false);

            return r;
        }

        public async Task<CompanyRegistrationRequest?> FindCompanyRegistrationRequestAsync(Expression<Func<CompanyRegistrationRequest, Boolean>> predicate)
        {
            var r = await this.Context.CompanyRegistrationRequests
                .Include(r => r.Associates)
                .Include(r => r.Locations)
                .ThenInclude(l => l.Owners)
                .SingleAsync(predicate, CancellationToken.None)
                .ConfigureAwait(false);

            return r;
        }

    }


}
