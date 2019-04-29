using Application.Accounts.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Accounts.Messages.Queries
{
    public class GeAccountsQuery
        :IRequest<IQueryable<AccountDto>>
    {
    }
}
