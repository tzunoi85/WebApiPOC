using System;
using System.Reflection;
using Application.Common;
using Autofac;
using Infrastructure.Common;
using Microsoft.Extensions.Configuration;
using Module = Autofac.Module;

namespace Infrastructure.Configuration.Autofac
{
    internal class ConfigurationsModule
        : Module
    {
        private readonly IConfiguration _configuration;

        public Assembly[] _assemblies
           => new[] { typeof(IApplication).Assembly, typeof(IInfrastructure).Assembly };

        public ConfigurationsModule(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new MediatorModule(_assemblies));
            builder.RegisterModule(new StorageModule(_configuration, _assemblies));
        }
    }
}
