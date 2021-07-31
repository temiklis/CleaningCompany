using CleaningCompany.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningCompany.Domain.Entities
{
    public class Order : Entity
    {
        public Status Status { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RenderedDate { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<Employee> ResponsibleEmployees { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
