using LT.DigitalOffice.Kernel.UnitTestLibrary;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Studfolio.UserService.Database;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Repositories;
using Studfolio.UserService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Studfolio.UserServiceUnitTests.Repositories
{
    public class UserRepositoryTests
    {
        private UserServiceDbContext dbContext;
        private IUserRepository repository;

        private DbUser dbUserToAdd;
        private DbUserCredentials dbUserCredentialsToAdd;
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

            dbUserToAdd = new DbUser
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
            dbUserCredentialsToAdd = new DbUserCredentials
            {
                Id = Guid.NewGuid(),
                UserId = dbUserToAdd.Id,
                PasswordHash = "pswd123456",
                Email = "example@mail.ru"
            };
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

        #region CreateUser
        [Test]
        public void ShouldReturnMatchingIdAndRightAddCompanyInDb()
        {
            var guidOfNewUser = repository.CreateUser(dbUserToAdd, dbUserCredentialsToAdd);

            Assert.AreEqual(dbUserToAdd.Id, guidOfNewUser);
            Assert.NotNull(dbContext.UserCredentials.Find(dbUserCredentialsToAdd.Id));
            Assert.NotNull(dbContext.Users.Find(dbUserToAdd.Id));
        }
        #endregion

        #region EditUser
        [Test]
        public void ShouldThrowExceptionIfUserWithRequiredIdDoesNotExistWhileEditingUser()
        {
            var user = new DbUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Status = "Example",
                IsActive = true,
            };

            Assert.Throws<Exception>(() => repository.EditUser(user));
            Assert.That(dbContext.Users.Find(dbUser.Id).Equals(dbUser));
            Assert.That(dbContext.Users, Is.EquivalentTo(new List<DbUser> { dbUser }));
        }

        [Test]
        public void ShouldEditUserWhenUserDataIsCorrect()
        {
            var local = dbContext.Users
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(dbUser.Id));
            dbContext.Entry(local).State = EntityState.Detached;

            var user = new DbUser
            {
                Id = dbUser.Id,
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Status = "Example",
                IsActive = true,
            };
            dbContext.Entry(user).State = EntityState.Modified;

            Assert.DoesNotThrow(() => repository.EditUser(user));
            Assert.That(dbContext.Users.Find(dbUser.Id).Equals(user));
            Assert.That(dbContext.Users, Is.EquivalentTo(new List<DbUser> { user }));
        }
        #endregion
    }
}