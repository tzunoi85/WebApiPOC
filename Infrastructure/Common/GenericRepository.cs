using System;
using Application.Common;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Context;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public abstract class GenericRepository<TEntity>
        : IGenericRepository<TEntity> where TEntity :class
    {
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
            => _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task Add(TEntity entity)
            => await _context.AddAsync(entity);

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAllAsQuaryable()
            => _context.Set<TEntity>().AsQueryable<TEntity>();

        public TEntity GetById(int key)
            => _context.Set<TEntity>().Find(key) ?? throw new KeyNotFoundException($"Entity with key: {key} not found!");

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(int key)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
