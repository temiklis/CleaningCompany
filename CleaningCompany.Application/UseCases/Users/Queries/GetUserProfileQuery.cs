using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Users.DTOs;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Users.Queries
{
    [Authorize()]
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
    }

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;
        private readonly ICurrentUserService _currentUserService;

        public GetUserProfileQueryHandler(IMapper mapper, IIdentityService identityService, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _identityService = identityService;
            _currentUserService = currentUserService;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var user = await _identityService.GetUserProfileAsync(userId);

            return _mapper.Map<UserProfileDto>(user);
        }
    }
}
