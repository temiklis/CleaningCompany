using System;
using System.Collections.Generic;

namespace CleaningCompany.Domain.Entities
{
    public class OrderRequest : Entity
    {
        public string Email { get; set; }
        public string FIO { get; set; }
        public string Address { get; set; }
        public DateTime RenderedDate { get; set; }
        public DateTime RequestedDate { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
