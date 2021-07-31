using System;
using System.Collections.Generic;

namespace CleaningCompany.Domain.Entities
{
    public class Employee : User
    {
        public DateTime HireDate { get; set; }
        public decimal SalaryPerHour { get; set; }
        public bool IsFired { get; set; }

        public ICollection<Order> AssignedOrders { get; set; }
    }
}
