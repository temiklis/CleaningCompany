using CleaningCompany.Domain.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleaningCompany.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal BasePrice { get; set; }
        public Difficulty Difficulty { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ICollection<OrderRequest> OrderRequests { get; set; }
        public ICollection<Material> Materials { get; set; }
    }
}
