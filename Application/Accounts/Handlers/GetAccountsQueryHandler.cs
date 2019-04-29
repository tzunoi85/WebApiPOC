using Application.Accounts.Dtos;
using Application.Accounts.Messages.Queries;
using Application.Accounts.Repositories;
using Application.Common;
using AutoMapper;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Accounts.Handlers
{
    public class GetAccountsQueryHandler
         : IRequestHandler<GeAccountsQuery, IQueryable<AccountDto>>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsQueryHandler(IMapper mapper, IAccountRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IQueryable<AccountDto>> Handle(GeAccountsQuery request, CancellationToken cancellationToken)
        {
            var rep = _unitOfWork.GetRepository<IAccountRepository>();
            var rep2 = _unitOfWork.GetRepository<IAccountRepository>();

            return _mapper.ProjectTo<AccountDto>(rep.GetAllAsQuaryable());
        }
    }
}
