using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Results;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Users.Commands
{
    [Authorize()]
    public class UpdateUserProfileCommand : IRequest<Result<bool>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public int Gender { get; set; }
    }

    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result<bool>>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        public UpdateUserProfileCommandHandler(ICurrentUserService currentUserService, IIdentityService identityService, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _identityService = identityService;
            _mapper = mapper;
        }

        public async Task<Result<bool>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var dbUser = await _identityService.GetUserProfileAsync(_currentUserService.UserId);

            _mapper.Map(request, dbUser);

            var result = await _identityService.UpdateUserProfile(dbUser);

            return result;
        }
    }
}
