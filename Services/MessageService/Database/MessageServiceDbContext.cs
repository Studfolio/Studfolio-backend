using Microsoft.EntityFrameworkCore;
using Studfolio.MessageService.Database.Entities;
using System.Reflection;

namespace Studfolio.MessageService.Database
{
    class MessageServiceDbContext : DbContext
    {
        public MessageServiceDbContext(DbContextOptions<MessageServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbEmail> Emails { get; set; }
        public DbSet<DbEmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
