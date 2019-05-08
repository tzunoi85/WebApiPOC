using System;
using System.Linq;
using Application.Accounts.Models;
using Application.Accounts.Repositories;
using Infrastructure.Common;
using Infrastructure.Context;

namespace Infrastructure.Accounts.Repositories
{
    public class AccountRepository
        : GenericRepository<Account>, IAccountRepository

    {
        public AccountRepository(ApplicationDbContext context) 
            : base(context)
        {
        }

        public IQueryable GetAllPermissionsAsQuaryable()
            => this._context.Set<AccountGroups>().AsQueryable();
    }
}
