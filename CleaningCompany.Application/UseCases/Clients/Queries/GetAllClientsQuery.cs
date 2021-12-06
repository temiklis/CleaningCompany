using AutoMapper;
using CleaningCompany.Application.Interfaces;
using CleaningCompany.Application.UseCases.Clients.DTOs;
using CleaningCompany.Domain.Entities;
using CleaningCompany.Results;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleaningCompany.Application.UseCases.Clients.Queries
{
    public class GetAllClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
        public ClientParameters Parameters { get; private set; }
        public GetAllClientsQuery(ClientParameters parameters)
        {
            Parameters = parameters;
        }
    }

    public class GetAllClientsQueryHandler : IRequestHandler<GetAllClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllClientsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ClientDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.Clients.GetAllClientsWithOrders();

            query = GetFilters(query, request.Parameters);

            var dtos = await PagedList<Client>.ToPagedList(query, request.Parameters.PageNumber, request.Parameters.PageSize,
                _mapper.Map<IEnumerable<ClientDto>>);

            return dtos;
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
