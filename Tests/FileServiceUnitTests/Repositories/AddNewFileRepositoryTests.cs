using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database;
using Studfolio.FileService.Database.Entities;
using System;

namespace Studfolio.FileService.Data.UnitTests
{
    public class AddNewFileRepositoryTests
    {
        private IFileRepository repository;
        private FileServiceDbContext dbContext;

        private DbFile newFile;

        [SetUp]
        public void SetUp()
        {
            var dbOptionsFileService = new DbContextOptionsBuilder<FileServiceDbContext>()
                .UseInMemoryDatabase("FileServiceTestDatabase")
                .Options;

            dbContext = new FileServiceDbContext(dbOptionsFileService);
            repository = new FileRepository(dbContext);

            newFile = new DbFile
            {
                Id = Guid.NewGuid(),
                Content = Convert.FromBase64String("RGlnaXRhbCBPZmA5Y2U="),
                Extension = ".txt",
                IsActive = true,
                Name = "DigitalOfficeTestFile"
            };
        }

        [Test]
        public void ShouldAddNewFileToDatabase()
        {
            Assert.AreEqual(newFile.Id, repository.AddNewFile(newFile));
            Assert.That(dbContext.Files.Find(newFile.Id), Is.EqualTo(newFile));
        }

        [Test]
        public void ShouldThrowArgumentExceptionWhenAddingFileWithRepeatingId()
        {
            repository.AddNewFile(newFile);

            Assert.Throws<ArgumentException>(() => repository.AddNewFile(newFile));
            Assert.That(dbContext.Files.Find(newFile.Id), Is.EqualTo(newFile));
        }

        [TearDown]
        public void CleanInMemoryDatabase()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }
    }
}
