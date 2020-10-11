using Microsoft.EntityFrameworkCore;
using Studfolio.VacancyService.Database.Entities;
using System.Reflection;

namespace Studfolio.VacancyService.Database
{
    class VacancyServiceDbContext : DbContext
    {
        public VacancyServiceDbContext(DbContextOptions<VacancyServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbVacancy> Vacancies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
