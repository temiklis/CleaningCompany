using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.DTOs
{
    public class CreateOrderDto
    {
        public int OrderRequestId { get; set; }
        public ICollection<string> Employees { get; set; }
    }
}
