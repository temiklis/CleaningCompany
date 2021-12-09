using System;

namespace CleaningCompany.Application.UseCases.Users.DTOs
{
    public class UpdateUserProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime? Birthday { get; set; }
        public int Gender { get; set; }
    }
}
