using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SRLRequest.Models;

namespace SRLRequest.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyRequestController : ControllerBase
    {
        [HttpPatch]
        public ActionResult<CompanyRequest> Patch([FromBody] JsonPatchDocument<CompanyRequest> patchRequest)
        {
            var request = new CompanyRequest();
            patchRequest.ApplyTo(request);

            return request;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyRequest>> Get()
        {
            return new ActionResult<IEnumerable<CompanyRequest>>(Enumerable.Empty<CompanyRequest>());
        }
    }
}
