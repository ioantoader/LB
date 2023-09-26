using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IT.DigitalCompany.Models;
using System.Collections.ObjectModel;
using IT.DigitalCompany.Infrastructure;
using System.ComponentModel.DataAnnotations;
using Duende.IdentityServer.Extensions;
using IT.DigitalCompany.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using System;

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
        public async Task<ActionResult<CompanyRegistrationRequest>> PutContact([FromRoute] Guid companyRequestId,
            Contact contact)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var registationRequest = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(companyRequestId)
                .ConfigureAwait(false);
            if (null == registationRequest)
            {
                return NotFound();
            }

            if(!String.Equals(registationRequest.UserId,User.GetSubjectId(), StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }

            await this._companyRegistrationManager.UpdateRegistrationRequestContactAsync(registationRequest, contact)
                .ConfigureAwait(false);
            return registationRequest;
        }

        [HttpPost("requests/{companyRequestId}/associates")]
        public async Task<ActionResult<Person>> PostAssociate([FromRoute] Guid companyRequestId,
            [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var r = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(companyRequestId)
                .ConfigureAwait(false);

            if (null == r)
                return NotFound();
            if (!String.Equals(r.UserId, User.GetSubjectId(), StringComparison.OrdinalIgnoreCase))
                return Forbid();

            await this._companyRegistrationManager
                .AddRegistrationRequestAssociateAsync(r,person)
                .ConfigureAwait(false);

            return person;
        }

        [HttpPut("requests/associates")]
        public async Task<ActionResult<Person>> PutAssociate([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var r = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(
                r => r.Associates.Any(a => a.Id.Equals(person.Id)))
                .ConfigureAwait(false);
            if (null == r)
                return NotFound();
            if (!String.Equals(r.UserId, this.User.GetSubjectId(),StringComparison.OrdinalIgnoreCase))
                return Forbid();
            await this._companyRegistrationManager.UpdateRegistrationRequestAssociateAsync(person)
                .ConfigureAwait(false);

            return person;
        }


        [HttpDelete("requests/associates/{id}")]
        public async Task<ActionResult<CompanyRegistrationRequest>> DeleteAssociate([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var r = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(
                r => r.Associates.Any(a => a.Id.Equals(id)))
                .ConfigureAwait(false);
            if (null == r)
                return NotFound();
            if (!String.Equals(r.UserId, this.User.GetSubjectId(), StringComparison.OrdinalIgnoreCase))
                return Forbid();
            await this._companyRegistrationManager.DeleteRegistrationRequestAssociateAsync(id)
                .ConfigureAwait(false);

            return r;
        }

        [HttpPost("requests/{companyRequestId}/locations")]
        public async Task<ActionResult<CompanyLocation>> PostLocation([FromRoute] Guid companyRequestId,
            [FromBody] CompanyLocation location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var r = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(companyRequestId)
                .ConfigureAwait(false);

            if (null == r)
                return NotFound();
            if (!String.Equals(r.UserId, User.GetSubjectId(), StringComparison.OrdinalIgnoreCase))
                return Forbid();

            await this._companyRegistrationManager
                .AddRegistrationRequestLocationAsync(r, location)
                .ConfigureAwait(false);

            return location;

        }

        [HttpGet("requests")]
        public ActionResult<IEnumerable<CompanyRegistrationRequest>> Get()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return new ActionResult<IEnumerable<CompanyRegistrationRequest>>(Enumerable.Empty<CompanyRegistrationRequest>());
        }

        [HttpPost("requests")]
        [Authorize()]
        public async Task<ActionResult<CompanyRegistrationRequest>> Post([FromBody] CompanyRegistrationRequest? request = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            request ??= new CompanyRegistrationRequest();

            var userId = request.UserId;
            if(String.IsNullOrWhiteSpace(userId))
            {
                userId = request.UserId = this.User.GetSubjectId();
            }
            if(!String.Equals(userId,request.UserId,StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError(nameof(request.UserId), "UserId is not equal with SubjetId");
                return BadRequest(ModelState);
            }
            var email = request.Contact?.Email;            
            if(String.IsNullOrWhiteSpace(email))
            {
                var contact = request.Contact ??= new Contact();
                contact.Email = this.User.GetEmail();
            }

            await this._companyRegistrationManager.CreateRegistrationRequestAsync(request)
                .ConfigureAwait(false);

            return request;
        }

    }
}
