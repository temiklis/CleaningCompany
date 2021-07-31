using CleaningCompany.Domain.Entities.Enums;
using System.Collections.Generic;

namespace CleaningCompany.Domain.Entities
{
    public class Client : User
    {
        public Discount Discount { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
