using Microsoft.AspNetCore.Mvc;
using IT.DigitalCompany.Models;
using IT.DigitalCompany.Infrastructure;
using Duende.IdentityServer.Extensions;
using IT.DigitalCompany.Extensions;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.EntityFrameworkCore;


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

        [Authorize()]
        [HttpPut("requests/{companyRequestId}/contact")]
        public async Task<ActionResult<CompanyRegistrationRequest>> PutContact([FromRoute] Guid companyRequestId,
            Contact contact)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var registationRequest = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(companyRequestId,
                includeAssociates: true,
                includeLocations: true,
                includeLocationsOwners: true)
                .ConfigureAwait(false);
            if (null == registationRequest)
            {
                return NotFound();
            }

            var subjectId = User.GetSubjectId();
            if (!String.Equals(registationRequest.UserId,subjectId, StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }

            await this._companyRegistrationManager.UpdateRegistrationRequestContactAsync(registationRequest, contact)
                .ConfigureAwait(false);
            return registationRequest;
        }

        [Authorize()]
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
            var subjectId = User.GetSubjectId();
            if (!String.Equals(r.UserId, subjectId, StringComparison.OrdinalIgnoreCase))
                return Forbid();

            await this._companyRegistrationManager
                .AddRegistrationRequestAssociateAsync(r,person)
                .ConfigureAwait(false);

            return person;
        }

        [Authorize()]
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
            var subjectId = User.GetSubjectId();
            if (!String.Equals(r.UserId, subjectId,StringComparison.OrdinalIgnoreCase))
                return Forbid();
            await this._companyRegistrationManager.UpdateRegistrationRequestAssociateAsync(person)
                .ConfigureAwait(false);

            return person;
        }


        [Authorize()]
        [HttpDelete("requests/associates/{id}")]
        public async Task<ActionResult<CompanyRegistrationRequest>> DeleteAssociate([FromRoute]Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var r = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(
                r => r.Associates.Any(a => a.Id.Equals(id)),
                includeAssociates: true, includeLocations: true, includeLocationsOwners: true)
                .ConfigureAwait(false);
            if (null == r)
                return NotFound();
            var subjectId = User.GetSubjectId();
            if (!String.Equals(r.UserId, subjectId, StringComparison.OrdinalIgnoreCase))
                return Forbid();
            await this._companyRegistrationManager.DeleteRegistrationRequestAssociateAsync(id)
                .ConfigureAwait(false);

            return r;
        }

        [Authorize()]
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
            var subjectId = User.GetSubjectId();
            if (!String.Equals(r.UserId, subjectId, StringComparison.OrdinalIgnoreCase))
                return Forbid();

            await this._companyRegistrationManager
                .AddRegistrationRequestLocationAsync(r, location)
                .ConfigureAwait(false);

            return location;

        }

        [Authorize()]
        [HttpPost("requests/locations")]
        public async Task<ActionResult<CompanyLocation>> putLocation(
            [FromBody] CompanyLocation location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(null == location)
                throw new ArgumentNullException(nameof(location));

            var r = await this._companyRegistrationManager.FindCompanyRegistrationRequestAsync(
                r => r.Locations.Any(l => l.Id.Equals(location.Id)))
                .ConfigureAwait(false);

            if (null == r)
                return NotFound();
            var subjectId = User.GetSubjectId();
            if (!String.Equals(r.UserId, subjectId, StringComparison.OrdinalIgnoreCase))
                return Forbid();

            await this._companyRegistrationManager
                .UpdateRegistrationRequestLocationAsync(location)
                .ConfigureAwait(false);

            return location;

        }
        
        [Authorize()]
        [HttpGet("requests")]
        public async Task<ActionResult<IEnumerable<CompanyRegistrationRequest>>> Get([FromQuery]Boolean? includeAssociates = true,
            [FromQuery]Boolean? includeLocations = true, [FromQuery] Boolean? includeLocationOwners = true)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subjectId = User.GetSubjectId();
            var query = this._companyRegistrationManager.CompanyRegistrationRequests
                .AsNoTracking()
                .Where(r => r.UserId.Equals(subjectId));
            if (includeAssociates.GetValueOrDefault())
            {
                query = query.Include(r => r.Associates)
                    .AsNoTracking();
            }
            if (includeLocations.GetValueOrDefault())
            {
                var t = query.Include(r => r.Locations);
                query = t.AsNoTracking();
                if(includeLocationOwners.GetValueOrDefault())
                {
                    query = t.ThenInclude(l => l.Owners)
                        .AsNoTracking();
                }
            }

            var l = await query.ToListAsync()
                .ConfigureAwait(false);
            return l;
        }

        [Authorize()]
        [HttpGet("requests/{requestId}")]
        public async Task<ActionResult<CompanyRegistrationRequest>> Get([FromRoute] Guid requestId,
            [FromQuery] Boolean? includeAssociates = true,
            [FromQuery] Boolean? includeLocations = true, [FromQuery] Boolean? includeLocationOwners = true)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.GetSubjectId();
            var r = await  this._companyRegistrationManager
                .FindCompanyRegistrationRequestAsync(requestId,
                includeAssociates: includeAssociates.GetValueOrDefault(),
                includeLocations: includeLocations.GetValueOrDefault(), includeLocationsOwners: includeLocationOwners.GetValueOrDefault())
                .ConfigureAwait(false);
          
            if (null == r)
                return NotFound();
            var subjectId = User.GetSubjectId();
            if (!String.Equals(r.UserId, subjectId, StringComparison.OrdinalIgnoreCase))
                return Forbid();

            return r;
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
            var subjectId = User.GetSubjectId();
            if (String.IsNullOrWhiteSpace(userId))
            {
                userId = request.UserId = subjectId;
            }
            if(!String.Equals(userId,subjectId,StringComparison.OrdinalIgnoreCase))
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
