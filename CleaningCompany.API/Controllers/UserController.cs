using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Users.Commands;
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
        private readonly IMediator _mediator;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IMediator mediator, ICurrentUserService currentUserService)
        {
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        [HttpGet("CurrentUserEmail")]
        public async Task<ActionResult<string>> GetCurrentUserEmail()
        {
            var query = new GetCurrentUserEmailQuery();

            var userEmail = await _mediator.Send(query);

            return Ok(userEmail);
        }

        [HttpGet("Roles")]
        public async Task<ActionResult<IReadOnlyCollection<string>>> GetCurrentUserRoles()
        {
            var query = new GetCurrentUserRolesQuery();

            var roles = await _mediator.Send(query);

            return Ok(roles);
        }

        [HttpGet]
        public async Task<ActionResult<UserProfileDto>> Get()
        {
            var query = new GetUserProfileQuery();

            var user = await _mediator.Send(query);

            return Ok(user);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateUserProfileDto dto)
        {
            var command = new UpdateUserProfileCommand()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Address = dto.Address,
                BirthDay = dto.Birthday,
                Gender = dto.Gender
            };

            var result = await _mediator.Send(command);

            return CreateResponseFromResult<bool>(result);
        }
    }
}
