using Moq;
using NUnit.Framework;
using Studfolio.UserService.Commands;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserServiceUnitTests.Commands
{
    public class DisableUserCommandTests
    {
        private Mock<IUserRepository> repositoryMock;
        private IDisableUserCommand command;

        private Guid userId;
        private DbUser newDbUser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            userId = Guid.NewGuid();

            newDbUser = new DbUser
            {
                Id = userId,
                FirstName = "Example1",
                LastName = "Example1",
                MiddleName = "Example1",
                Status = "normal",
                AvatarFileId = Guid.NewGuid(),
                IsActive = true,
            };
        }

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IUserRepository>();
            command = new DisableUserCommand(repositoryMock.Object);
        }

        [Test]
        public void ShouldThrowExceptionWhenUserIdNotFoundInDb()
        {
            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(userId));
            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Never);
        }

        [Test]
        public void ShouldThrowExceptionWhenEditingUserInDb()
        {
            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Returns(newDbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(userId));
            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Once);
        }

        [Test]
        public void ShouldDisableUser()
        {
            repositoryMock
                .Setup(x => x.GetUserInfoById(userId))
                .Returns(newDbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()));

            command.Execute(userId);

            repositoryMock.Verify(repository => repository.GetUserInfoById(userId), Times.Once);
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Once);
        }
    }
}
