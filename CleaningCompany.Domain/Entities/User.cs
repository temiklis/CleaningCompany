using CleaningCompany.Domain.Entities.Enums;
using System;

namespace CleaningCompany.Domain.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }
}
