using Microsoft.EntityFrameworkCore;
using Studfolio.FileService.Database.Entities;

namespace Studfolio.FileService.Database
{
    class FileServiceDbContext : DbContext
    {
        public FileServiceDbContext(DbContextOptions<FileServiceDbContext> options)
            : base(options)
        {
        }

        public DbSet<DBFile> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}