using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Users.DTOs;
using CleaningCompany.Application.UseCases.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;

        public UserController(IIdentityService identityService, IMediator mediator)
        {
            _identityService = identityService;
            _mediator = mediator;
        }

        [HttpGet("CurrentUserEmail")]
        public ActionResult<string> GetCurrentUserEmail()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<List<string>>> GetCurrentUserRoles()
        {
            var user = User;
            var claims = User.Claims;
            var identity = User.Identity;
            var userName = User.Identity.Name;
            userName = "";

            var roles = await _identityService.GetUserRoles(userName);

            return Ok(roles);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserProfileDto>> Get([FromRoute] string userId)
        {
            var user = await _mediator.Send(new GetUserProfileQuery() { UserId = userId });

            return Ok(user);
        }
    }
}
