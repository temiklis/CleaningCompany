using AutoMapper;
using CleaningCompany.Application.Common.Security;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Clients.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Domain.Entities.Enums;
using CleaningCompany.Results;
using CleaningCompany.Results.Implementations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Clients.Queries
{
    [Authorize(Roles = "Admin")]
    public class GetAllClientsQuery : IRequest<Result<PagedList<ClientDto>>>
    {
        public ClientParameters Parameters { get; private set; }
        public GetAllClientsQuery(ClientParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, Result<PagedList<ClientDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllClientsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<PagedList<ClientDto>>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Clients.GetAllClientsWithOrders();

            query = GetFilters(query, request.Parameters);

            var dtos = await PagedList<Client>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<ClientDto>>);

            return new SuccessResult<PagedList<ClientDto>>(dtos);
        }

        private static IQueryable<Client> GetFilters(IQueryable<Client> query, ClientParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Name))
            {
                query = query.Where(s => s.FirstName.ToLower().Contains(parameters.Name.ToLower())
                || s.LastName.ToLower().Contains(parameters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(parameters.Email))
            {
                query = query.Where(s => s.Email.ToLower().Contains(parameters.Email.ToLower()));
            }

            return query;
        }
    }
}
