using System;

namespace CleaningCompany.Application.UseCases.Users.DTOs
{
    public class UserProfileDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; } 
    }
}
