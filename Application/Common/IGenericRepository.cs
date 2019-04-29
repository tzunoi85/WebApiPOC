using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IGenericRepository<TEntity> 
        : IRepository where TEntity : class
    {

        IEnumerable<TEntity> GetAll();
        IQueryable<TEntity> GetAllAsQuaryable();
        TEntity GetById(int key);

        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        void Remove(int key);


    }
}
