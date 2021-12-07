using CleaningCompany.Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleaningCompany.Domain.Entities
{
    public class Order : Entity
    {
        public Status Status { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RenderedDate { get; set; }

        public string ClientId { get; set; }
        public Client Client { get; set; }

        public ICollection<Employee> ResponsibleEmployees { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
