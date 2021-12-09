using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
