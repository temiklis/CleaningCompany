using CleaningCompany.Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System;

namespace CleaningCompany.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
