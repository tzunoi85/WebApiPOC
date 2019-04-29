using Application.Accounts.Dtos;
using MediatR;

namespace Application.Accounts.Messages.Queries
{
    public class GetAccountByIdQuery
        :IRequest<AccountDto>
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
