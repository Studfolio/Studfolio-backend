using LT.DigitalOffice.Kernel.Exceptions;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database;
using Studfolio.FileService.Database.Entities;
using System;
using System.Linq;

namespace Studfolio.FileService.Data
{
    /// <inheritdoc cref="IFileRepository"/>
    public class FileRepository : IFileRepository
    {
        private readonly FileServiceDbContext dbContext;

        public FileRepository(FileServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid AddNewFile(DbFile file)
        {
            dbContext.Files.Add(file);
            dbContext.SaveChanges();

            return file.Id;
        }

        public void DisableFileById(Guid fileId)
        {
            var dbFile = dbContext.Files.FirstOrDefault(file => file.Id == fileId);

            if (dbFile == null)
            {
                throw new NotFoundException("File with this id was not found.");
            }

            dbFile.IsActive = false;

            dbContext.Files.Update(dbFile);
            dbContext.SaveChanges();
        }

        public DbFile GetFileById(Guid fileId)
        {
            var dbFile = dbContext.Files.FirstOrDefault(file => file.Id == fileId);

            if (dbFile == null)
            {
                throw new NotFoundException("File with this id was not found.");
            }

            return dbFile;
        }
    }
}
