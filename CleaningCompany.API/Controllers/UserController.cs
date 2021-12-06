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
        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
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
    }
}
