using LT.DigitalOffice.Kernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database;
using Studfolio.FileService.Database.Entities;
using System;

namespace Studfolio.FileService.Data.UnitTests
{
    class DisableFileByIdRepositoryTests
    {
        private FileServiceDbContext dbContext;
        private IFileRepository repository;
        private Guid fileId;
        private DbFile dbFile;

        [SetUp]
        public void Setup()
        {
            var dbOptions = new DbContextOptionsBuilder<FileServiceDbContext>()
                                    .UseInMemoryDatabase("InMemoryDatabase")
                                    .Options;
            dbContext = new FileServiceDbContext(dbOptions);
            repository = new FileRepository(dbContext);

            fileId = Guid.NewGuid();
            dbFile = new DbFile
            {
                Id = fileId,
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
        public void ShouldThrowExceptionWhenFileWasNotFound()
        {
            Assert.Throws<NotFoundException>(() => repository.DisableFileById(Guid.NewGuid()));
        }

        [Test]
        public void ShouldDisableFile()
        {
            repository.DisableFileById(dbFile.Id);

            Assert.That(dbContext.Files.Find(fileId).IsActive == false);
        }
    }
}
