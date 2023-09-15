using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SRLRequest.Models;
using System.Collections.ObjectModel;

namespace SRLRequest.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRequestController : ControllerBase
    {
        [HttpPut("{companyRequestId}/contact")]
        public ActionResult<CompanyRequest> PutContact([FromRoute] Guid companyRequestId,
            Contact contact)
        {
            var request = new CompanyRequest();
            request.Contact = contact;

            return request;
        }

        [HttpPost("{companyRequestId}/associates")]
        public ActionResult<CompanyRequest> PostAssociate([FromRoute] Guid companyRequestId,
            [FromBody] Person person)
        {
            var request = new CompanyRequest();

            var col = request.Associates ??= new Collection<Person>();
            col.Add(person);
            person.Id = Guid.NewGuid();
            return request;
        }

        [HttpPost("associates")]
        public ActionResult<CompanyRequest> PutAssociate([FromBody] Person person)
        {
            var request = new CompanyRequest();

            var col = request.Associates ??= new Collection<Person>();
            col.Add(person);            
            return request;
        }

        [HttpDelete("associates/{id}")]
        public ActionResult<CompanyRequest> DeleteAssociate([FromRoute]Guid id)
        {
            var request = new CompanyRequest();

            var col = request.Associates ??= new Collection<Person>();            
            return request;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyRequest>> Get()
        {
            return new ActionResult<IEnumerable<CompanyRequest>>(Enumerable.Empty<CompanyRequest>());
        }
    }
}
