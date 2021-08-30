using CleaningCompany.Result;
using MediatR;

namespace CleaningCompany.Application.UseCases.Orders.Commands
{
    public class CreateOrderCommand : IRequest<Result<int>>
    {

    }
}
