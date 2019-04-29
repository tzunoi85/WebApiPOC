using Application.Accounts.Models;
using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Repositories
{
    public interface IAccountRepository
        : IGenericRepository<Account>
    {
         
    }
}
