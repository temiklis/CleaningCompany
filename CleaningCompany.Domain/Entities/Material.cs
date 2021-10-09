using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleaningCompany.Domain.Entities
{
    public class Material : Entity
    {
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
