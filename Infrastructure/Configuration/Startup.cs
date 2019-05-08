using System;
using Application.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Infrastructure.Configuration;
using Infrastructure.Configuration.Autofac;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                    .AddAuthorization()
                    .AddFluentValidation(opt => opt.RegisterValidatorsFromAssembly(typeof(IApplication).Assembly))
                    .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1)
                    .AddFormatterMappings()
                    .AddJsonFormatters();

            var key = Encoding.ASCII.GetBytes("MySecretKeyMySecretKey");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

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

            app.UseAuthentication();

            app.UseMvc(opt =>
            {
                opt.EnableDependencyInjection();
                opt.Expand().Select().OrderBy().Filter().MaxTop(10).Count();
                });
           
            app.UseMvcWithDefaultRoute();
           
        }
    }
}
