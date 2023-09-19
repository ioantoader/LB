using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IT.DigitalCompany.Models;
using System.Collections.ObjectModel;
using IT.DigitalCompany.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace IT.DigitalCompany.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRegistrationController : ControllerBase
    {
        public readonly CompanyRegistrationManager _companyRegistrationManager;
        public CompanyRegistrationController(CompanyRegistrationManager companyRegistrationManager) 
        {
            _companyRegistrationManager = companyRegistrationManager ?? throw new ArgumentNullException(nameof(companyRegistrationManager));
        }
        [HttpPut("requests/{companyRequestId}/contact")]
        public ActionResult<CompanyRegistrationRequest> PutContact([FromRoute] Guid companyRequestId,
            Contact contact)
        {
            var request = new CompanyRegistrationRequest();
            request.Contact = contact;

            return request;
        }

        [HttpPost("requests/{companyRequestId}/associates")]
        public ActionResult<CompanyRegistrationRequest> PostAssociate([FromRoute] Guid companyRequestId,
            [FromBody] Person person)
        {
            var request = new CompanyRegistrationRequest();

            var col = request.Associates ??= new Collection<Person>();
            col.Add(person);
            person.Id = Guid.NewGuid();
            return request;
        }

        [HttpPost("requests/associates")]
        public ActionResult<CompanyRegistrationRequest> PutAssociate([FromBody] Person person)
        {
            var request = new CompanyRegistrationRequest();

            var col = request.Associates ??= new Collection<Person>();
            col.Add(person);            
            return request;
        }

        [HttpDelete("requests/associates/{id}")]
        public ActionResult<CompanyRegistrationRequest> DeleteAssociate([FromRoute]Guid id)
        {
            var request = new CompanyRegistrationRequest();

            var col = request.Associates ??= new Collection<Person>();            
            return request;
        }

        [HttpGet("requests")]
        public ActionResult<IEnumerable<CompanyRegistrationRequest>> Get()
        {
            return new ActionResult<IEnumerable<CompanyRegistrationRequest>>(Enumerable.Empty<CompanyRegistrationRequest>());
        }

        [HttpPost("requests")]
        public ActionResult<CompanyRegistrationRequest> Post([FromBody] CompanyRegistrationRequest? request = null)
        {
            request ??= new CompanyRegistrationRequest();
            var email = request.Contact?.Email;            
            if(String.IsNullOrWhiteSpace(email))
            {
                var contact = request.Contact ??= new Contact();
                contact.Email = email;
            }
            
            return request;
        }

    }
}
