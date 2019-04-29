using Application.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Common;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Autofac;
using MediatR;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Infrastructure
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
       

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                    .AddFluentValidation(opt => opt.RegisterValidatorsFromAssembly(typeof(IApplication).Assembly))
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
                    .AddFormatterMappings()
                    .AddJsonFormatters();

            services.AddAutoMapper();
            services.AddOData();
            services.CreateDatabase(Configuration);

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new ConfigurationsModule(Configuration));

            this.ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(this.ApplicationContainer);

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseMvc(opt =>
            {
                opt.EnableDependencyInjection();
                opt.Expand().Select().OrderBy().Filter().MaxTop(10).Count();
                });

            app.UseMvcWithDefaultRoute();
           
        }
    }
}
