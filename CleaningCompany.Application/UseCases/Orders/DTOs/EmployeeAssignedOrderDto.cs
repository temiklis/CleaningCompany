using System;
using System.Collections.Generic;

namespace CleaningCompany.Application.UseCases.Orders.DTOs
{
    public class EmployeeAssignedOrderDto
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime RenderedDate { get; set; }
        public DateTime OrderDate { get; set; }
        public string Products { get; set; }
        public string Status { get; set; }
        public decimal TotalIncome { get; set; }
        public int EmployeesAmount { get; set; }
        public string Address { get; set; }

        public List<EmployeeOrderProductDto> ProductsList { get; set; }
    }
}
