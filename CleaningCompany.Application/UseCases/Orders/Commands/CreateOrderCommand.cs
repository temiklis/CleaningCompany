using CleaningCompany.Results;
using MediatR;

namespace CleaningCompany.Application.UseCases.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Result<int>>
    {

    }
}
