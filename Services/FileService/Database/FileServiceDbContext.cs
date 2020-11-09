using Microsoft.EntityFrameworkCore;
using Studfolio.FileService.Database.Entities;

namespace Studfolio.FileService.Database
{
    public class FileServiceDbContext : DbContext
    {
        public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<DbFile> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}