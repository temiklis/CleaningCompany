using CleaningCompany.Application.Interfaces;
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
        ICurrentUserService _currentUserService;

        public UserController(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
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
            var userName = _currentUserService.UserId;
            var roles = await _identityService.GetUserRoles(userName);

            return Ok(roles);
        }
    }
}
