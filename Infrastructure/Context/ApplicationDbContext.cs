using Application.Accounts.Models;
using Application.Accounts.Repositories;
using Infrastructure.Context.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class ApplicationDbContext
        : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountGroups> AccountGroups { get; set; }

        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new AccountGroupsConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
        }

    }
}
