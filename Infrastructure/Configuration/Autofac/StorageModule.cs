using System;
using System.Linq;
using System.Reflection;
using Application.Common;
using Autofac;
using Infrastructure.Common;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace Infrastructure.Configuration.Autofac
{
    public class StorageModule
        :Module
    {
        private readonly Assembly[] _assemblies;
        private readonly IConfiguration _configuration;

        public StorageModule(IConfiguration configuration, params Assembly[] assemblies)
        {
            _assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void Load(ContainerBuilder builder)
        {

            builder.Register<ApplicationDbContext>(c => CreateDbContext())
              .AsSelf()
              .InstancePerDependency();

            builder.RegisterAssemblyTypes(_assemblies)
                .Where(t => !t.IsAbstract &&
                             t.GetInterfaces().Any(i => i.IsGenericType &&
                                                        i.GetGenericTypeDefinition() == typeof(IGenericRepository<>)))
                .As(t => t.GetInterfaces().First(i => !i.IsGenericType && i != typeof(IRepository)))
                .InstancePerDependency();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerDependency();
        }

        private ApplicationDbContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);
            return new ApplicationDbContext(builder.Options);
        }
    }
}
