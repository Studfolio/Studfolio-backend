using Microsoft.EntityFrameworkCore;
using Studfolio.CompanyService.Database.Entities;
using System.Reflection;

namespace Studfolio.CompanyService.Database
{
    class CompanyServiceDbContext : DbContext
    {
        public CompanyServiceDbContext(DbContextOptions<CompanyServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbCompany> Companies { get; set; }
        public DbSet<DbCompanyCredentials> CompanyCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
