using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public UserController()
        {

        }

        [HttpGet("CurrentUserEmail")]
        public ActionResult<string> GetCurrentUserEmail()
        {
            var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (claim != null)
                return claim.Value;
            return string.Empty;
        }
    }
}
