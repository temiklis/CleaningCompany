using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Users.Queries
{
    [Authorize()]
    public class GetCurrentUserRolesQuery : IRequest<IReadOnlyCollection<string>>
    {

    }

    public class GetCurrentUserRolesQueryHandler : IRequestHandler<GetCurrentUserRolesQuery, IReadOnlyCollection<string>>
    {
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;
        public GetCurrentUserRolesQueryHandler(IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<IReadOnlyCollection<string>> Handle(GetCurrentUserRolesQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            var roles = await _identityService.GetUserRoles(userId);

            return roles.AsReadOnly();
        }
    }
}
