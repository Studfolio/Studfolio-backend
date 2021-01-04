using Microsoft.EntityFrameworkCore;
using Studfolio.CVService.Database.Entities;
using System.Reflection;

namespace Studfolio.CVService.Database
{
    class CVServiceDbContext : DbContext
    {
        public CVServiceDbContext(DbContextOptions<CVServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbCV> CVs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}