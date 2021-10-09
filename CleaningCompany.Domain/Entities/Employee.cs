using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleaningCompany.Domain.Entities
{
    public class Employee : User
    {
        public DateTime HireDate { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal SalaryPerHour { get; set; }
        public bool IsFired { get; set; }

        public ICollection<Order> AssignedOrders { get; set; }
    }
}
