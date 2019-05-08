using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Accounts.Dtos;
using Application.Accounts.Messages.Queries;
using Application.Accounts.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Accounts.Handlers
{
    public class GePermissionsQueryHandler
        :  IRequestHandler<GePermissionsQuery, IQueryable<PermissionsDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;

        public GePermissionsQueryHandler(IMapper mapper, IAccountRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IQueryable<PermissionsDto>> Handle(GePermissionsQuery request, CancellationToken cancellationToken)
            => _mapper.ProjectTo<PermissionsDto>(_repository.GetAllPermissionsAsQuaryable());
    }
}
