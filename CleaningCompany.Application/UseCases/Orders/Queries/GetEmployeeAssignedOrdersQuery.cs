using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Orders.DTOs;
using CleaningCompany.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Orders.Queries
{
    [Authorize(Roles ="Employee,Admin")]
    public class GetEmployeeAssignedOrdersQuery : IRequest<IEnumerable<EmployeeAssignedOrderDto>>
    {

    }

    public class GetEmployeeAssignedOrdersQueryHandler : IRequestHandler<GetEmployeeAssignedOrdersQuery, IEnumerable<EmployeeAssignedOrderDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public GetEmployeeAssignedOrdersQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<EmployeeAssignedOrderDto>> Handle(GetEmployeeAssignedOrdersQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            var query = _unitOfWork.Orders.GetEmployeeAssignedOrders(userId);
            var orders = _mapper.Map<IEnumerable<EmployeeAssignedOrderDto>>(query);

            foreach (var order in orders)
            {
                if (order.Status == Enum.GetName(typeof(Status), Status.Completed))
                {
                    CalculateOrderIncome(order);
                }
            }

            return orders;
        }

        private void CalculateOrderIncome(EmployeeAssignedOrderDto order)
        {
            foreach (var product in order.ProductsList)
            {
                var incomePercentage = (int)((Income)Enum.Parse(typeof(Income), product.Difficulty));
                order.TotalIncome += (product.BasePrice * incomePercentage) / (100 * order.EmployeesAmount);
            }
        }
    }
}
