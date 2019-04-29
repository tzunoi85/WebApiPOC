using Application.Accounts.Messages.Commands;
using Application.Accounts.Models;
using Application.Accounts.Repositories;
using AutoMapper;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Accounts.Handlers
{
    public class CreateAccountCommandHandler
        : AsyncRequestHandler<CreateAccountCommand>
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _repository;

        public CreateAccountCommandHandler(IMapper mapper, IAccountRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        protected override async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
                var account = _mapper.Map<CreateAccountCommand, Account>(request);
                await _repository.Add(account);
        }
    }
}
