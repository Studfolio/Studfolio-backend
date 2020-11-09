using LT.DigitalOffice.Kernel.Exceptions;
using LT.DigitalOffice.UnitTestKernel;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database;
using Studfolio.FileService.Database.Entities;
using System;

namespace Studfolio.FileService.Data.UnitTests
{
    public class GetFileByIdRepositoryTests
    {
        private FileServiceDbContext dbContext;
        private IFileRepository repository;

        private DbFile dbFile;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<FileServiceDbContext>()
                                    .UseInMemoryDatabase("InMemoryDatabase")
                                    .Options;
            dbContext = new FileServiceDbContext(dbOptions);
            repository = new FileRepository(dbContext);

            dbFile = new DbFile
            {
                Id = Guid.NewGuid(),
                Content = Convert.FromBase64String("RGlnaXRhbCBPZmA5Y2U="),
                Extension = ".txt",
                IsActive = true,
                Name = "DigitalOfficeTestFile"
            };

            dbContext.Files.Add(dbFile);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void Clean()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        [Test]
        public void ShouldThrowExceptionWhenThereNoFileInDatabaseWithSuchId()
        {
            Assert.Throws<NotFoundException>(() => repository.GetFileById(Guid.NewGuid()));
        }

        [Test]
        public void ShouldReturnFileInfoWhenGettingFileById()
        {
            var result = repository.GetFileById(dbFile.Id);

            var expected = new DbFile
            {
                Id = dbFile.Id,
                Name = dbFile.Name,
                Content = dbFile.Content,
                Extension = dbFile.Extension,
                IsActive = dbFile.IsActive
            };

            SerializerAssert.AreEqual(expected, result);
        }
    }
}