using System;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration
{
    public static class DatabaseConfigration
    {
        public static void  CreateDatabase(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var context = CreateDbContext(configuration);
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }

        private static ApplicationDbContext CreateDbContext(IConfiguration configuration)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
            return new ApplicationDbContext(builder.Options);
        }
    }
}
