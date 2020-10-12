using Microsoft.EntityFrameworkCore;
using Studfolio.PortfolioService.Database.Entities;

namespace Studfolio.PortfolioService.Database
{
    class PortfolioServiceDbContext : DbContext
    {
        public PortfolioServiceDbContext(DbContextOptions<PortfolioServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbPortfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}