using System;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IUnitOfWork
    {

        TRepository GetRepository<TRepository>() where TRepository: IRepository;

        Task SaveChanges();

    }
}
