using AutoMapper;
using CleaningCompany.Application.UseCases.Users.Commands;
using CleaningCompany.Application.UseCases.Users.DTOs;
using CleaningCompany.Domain.Entities;

namespace CleaningCompany.Application.UseCases.Users
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserProfileDto>();
            CreateMap<UpdateUserProfileCommand, User>(MemberList.Source);
        }
    }
}
