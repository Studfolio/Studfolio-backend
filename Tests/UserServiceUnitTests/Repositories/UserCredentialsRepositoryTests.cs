using LT.DigitalOffice.Kernel.Exceptions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Studfolio.UserService.Database;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserServiceUnitTests.Repositories
{
    public class UserCredentialsRepositoryTests
    {
        private UserServiceDbContext dbContext;
        private IUserCredentialsRepository repository;
        private Guid userId;
        private string salt;
        private DbUserCredentials userCredentials;

        private void GetMemoryContext()
        {
            var options = new DbContextOptionsBuilder<UserServiceDbContext>()
                .UseInMemoryDatabase("InMemoryDatabase")
                .Options;

            dbContext = new UserServiceDbContext(options);
        }

        [SetUp]
        public void SetUp()
        {
            userId = Guid.NewGuid();
            salt = Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

            userCredentials = new DbUserCredentials
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Email = "email@gmail.com",
                PasswordHash = UserPassword.GetPasswordHash("email@gmail.com", salt, "Password1111"),
                Salt = salt
            };

            GetMemoryContext();

            repository = new UserCredentialsRepository(dbContext);

            dbContext.UserCredentials.Add(userCredentials);
            dbContext.SaveChanges();
        }

        #region ChangePassword
        [Test]
        public void ShouldThrowExceptionWhenUserCredentialsDoesNotFound()
        {
            Assert.Throws<NotFoundException>(() => repository.ChangePassword("login12345", "newPassword1"));
        }

        [Test]
        public void ShouldThrowExceptionWhenRequestDataIsEmpty()
        {
            Assert.Throws<BadRequestException>(() => repository.ChangePassword(null, "newPassword"));
        }

        [Test]
        public void ShouldChangePassword()
        {
            var newPassword = "newPassword123456";

            Assert.DoesNotThrow(() => repository.ChangePassword(userCredentials.Email, newPassword));
            Assert.AreEqual(UserPassword.GetPasswordHash(userCredentials.Email, userCredentials.Salt, newPassword), userCredentials.PasswordHash);
        }
        #endregion
    }
}
