using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Users.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Users.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileDto>
    {
        public string UserId { get; set; }
    }

    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileDto>
    {
        private readonly IMapper _mapper;
        private readonly IIdentityService _identityService;

        public GetUserProfileQueryHandler(IMapper mapper, IIdentityService identityService)
        {
            _mapper = mapper;
            _identityService = identityService;
        }

        public async Task<UserProfileDto> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _identityService.GetUserProfileAsync(request.UserId);

            return _mapper.Map<UserProfileDto>(user);
        }
    }
}
