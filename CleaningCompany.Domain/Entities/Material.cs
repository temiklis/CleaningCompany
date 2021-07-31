using System.Collections.Generic;

namespace CleaningCompany.Domain.Entities
{
    public class Material : Entity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
