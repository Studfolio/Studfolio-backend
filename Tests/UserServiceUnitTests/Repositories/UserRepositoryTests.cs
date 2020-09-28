using LT.DigitalOffice.Kernel.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Studfolio.UserService.Database;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Repositories;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserServiceUnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private UserServiceDbContext dbContext;
        private IUserRepository repository;

        private DbUser dbUser;

        private UserServiceDbContext GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<UserServiceDbContext>()
                .UseInMemoryDatabase("InMemoryDatabase")
                .Options;

            return new UserServiceDbContext(options);
        }

        [SetUp]
        public void SetUp()
        {
            dbContext = GetMemoryContext();
            repository = new UserRepository(dbContext);

            dbUser = new DbUser
            {
                Id = Guid.NewGuid(),
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName",
                Role = 0,
                AvatarFileId = Guid.NewGuid(),
                Status = "Status",
                OrganizationId = Guid.NewGuid(),
                OrganizationName = "OrganizationName",
                Verified = true,
                IsActive = true
            };

            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        #region GetUserInfoById
        [Test]
        public void ShouldThrowExceptionWhenUserWithRequiredIdDoesNotExist()
        {
            Assert.That(() => repository.GetUserInfoById(Guid.Empty),
                Throws.TypeOf<Exception>());
        }

        [Test]
        public void ShouldReturnUserWhenUserWithRequiredIdExists()
        {
            var resultUser = repository.GetUserInfoById(dbUser.Id);

            Assert.IsInstanceOf<DbUser>(resultUser);
            SerializerAssert.AreEqual(dbUser, resultUser);
            Assert.That(dbContext.Users, Is.EquivalentTo(new[] { dbUser }));
        }
        #endregion
    }
}