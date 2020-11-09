using Studfolio.FileService.Database.Entities;
using Studfolio.FileService.Mappers.Interfaces;
using Studfolio.FileService.Models.Dto;
using System;

namespace Studfolio.FileService.Mappers
{
    public class FileMapper : IMapper<DbFile, File>, IMapper<FileCreateRequest, DbFile>
    {
        public File Map(DbFile dbFile)
        {
            if (dbFile == null)
            {
                throw new ArgumentNullException(nameof(dbFile));
            }

            return new File()
            {
                Id = dbFile.Id,
                Content = Convert.ToBase64String(dbFile.Content),
                Extension = dbFile.Extension,
                Name = dbFile.Name
            };
        }

        public DbFile Map(FileCreateRequest file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }

            return new DbFile()
            {
                Content = Convert.FromBase64String(file.Content),
                Extension = file.Extension,
                Name = file.Name,
                IsActive = true
            };
        }
    }
}