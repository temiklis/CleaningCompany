using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Users.Queries
{
    [Authorize()]
    public class GetCurrentUserEmailQuery : IRequest<string>
    {
    }

    public class GetCurrentUserEmailQueryHandler : IRequestHandler<GetCurrentUserEmailQuery, string>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        public GetCurrentUserEmailQueryHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }
        public async Task<string> Handle(GetCurrentUserEmailQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var userEmail = await _identityService.GetUserEmailAsync(userId);

            return userEmail;
        }
    }
}
