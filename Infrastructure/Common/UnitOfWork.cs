using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common;
using Autofac;
using Infrastructure.Context;

namespace Infrastructure.Common
{
    public class UnitOfWork :
        IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IComponentContext _componentContext;

        private Dictionary<Type, IRepository> _repositories;

        public UnitOfWork(ApplicationDbContext context, IComponentContext componentContext)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _componentContext = componentContext ?? throw new ArgumentNullException(nameof(componentContext));
            _repositories = new Dictionary<Type, IRepository>();
        }

        public TRepository GetRepository<TRepository>() where TRepository : IRepository
        {
            if(!_repositories.ContainsKey(typeof(TRepository)))
                _repositories.Add(typeof(TRepository), 
                    _componentContext.Resolve<TRepository>(new TypedParameter(typeof(ApplicationDbContext), _context)));

            return  (TRepository)_repositories[typeof(TRepository)];
        }

        public async Task SaveChanges()
            => await _context.SaveChangesAsync();
    }
}
