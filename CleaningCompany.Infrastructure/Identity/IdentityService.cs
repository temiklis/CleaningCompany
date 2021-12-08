using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;

        public IdentityService(UserManager<User> userManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result<string>> CreateUserAsync(string userName, string password)
        {
            var user = new User
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return new ErrorResult<string>("Error occured while creating the user");
            }

            return new SuccessResult<string>(user.Id);
        }

        public async Task<Result<bool>> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return new SuccessResult<bool>(true);
        }

        private async Task<Result<bool>> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                return new ErrorResult<bool>("Error occured while deleting the user");
            }

            return new SuccessResult<bool>(true);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<string> GetUserEmailAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.Email;
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return await _userManager.IsInRoleAsync(user, role);
        }

        //public async Task<List<string>> GetUserRoles(string userName)
        //{
        //    var user = _httpContextAccessor.HttpContext?.User;
        //    var claims = user.Claims;
        //    var identity = user.Identity;
        //    var userId = user?.FindFirst(c => c.Value == ClaimTypes.NameIdentifier)?.Value;

        //    var dbUser = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        //    var roles = await _userManager.GetRolesAsync(dbUser);

        //    return roles.ToList();
        //}

        public async Task<List<string>> GetUserRoles(string userId)
        {
            var user =  _userManager.Users.SingleOrDefault(u => u.Id == userId);

            var roles = await _userManager.GetRolesAsync(user);

            return roles.ToList();
        }

        public  async Task<User> GetUserProfileAsync(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(user => user.Id == userId);

            return user;
        }
    }
}
