using Application.Accounts.Models;
using Application.Common;
using System.Linq;

namespace Application.Accounts.Repositories
{
    public interface IAccountRepository
        : IGenericRepository<Account>
    {
        IQueryable GetAllPermissionsAsQuaryable();
    }
}
