using Autofac;
using MediatR;
using System.Linq;
using System.Reflection;
using Module = Autofac.Module;

namespace Infrastructure.Configuration.Autofac
{
    internal class MediatorModule
        : Module
    {
        private readonly Assembly[] _assemblies;

        public MediatorModule(params Assembly[] assemblies)
            => _assemblies = assemblies;

        protected override void Load(ContainerBuilder builder)
        {
            builder
             .RegisterAssemblyTypes(_assemblies)
             .Where(t => !t.IsAbstract && t.IsPublic)
             .AsClosedTypesOf(typeof(IRequestHandler<,>));

            builder
                .RegisterAssemblyTypes(_assemblies)
                .Where(t => !t.IsAbstract && t.IsPublic)
                .AsClosedTypesOf(typeof(INotificationHandler<>));


            _assemblies.SelectMany(a => a.GetTypes()
                       .Where(t => !t.IsAbstract &&
                                    t.IsPublic &&
                                    t.GetInterfaces().Where(i => i.IsGenericType)
                                                      .Any(i => i.GetGenericTypeDefinition() == typeof(IPipelineBehavior<,>))))
                       .ToList()
                       .ForEach(t => builder.RegisterGeneric(t).As(typeof(IPipelineBehavior<,>)));

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerDependency();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}
