using FluentValidation;
using Moq;
using NUnit.Framework;
using Studfolio.UserService.Commands;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserServiceUnitTests.Commands
{
    public class CreateUserCommandTests
    {
        private Mock<IValidator<UserRequest>> validatorMock;
        private Mock<IMapper<UserRequest, DbUser>> mapperMock;
        private Mock<IMapper<UserRequest, DbUserCredentials>> mapperCredentialsMock;
        private Mock<IUserRepository> repositoryMock;

        private ICreateUserCommand command;
        private UserRequest request;
        private Guid userId;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            userId = Guid.NewGuid();
            request = new UserRequest();
        }

        [SetUp]
        public void SetUp()
        {
            validatorMock = new Mock<IValidator<UserRequest>>();
            mapperMock = new Mock<IMapper<UserRequest, DbUser>>();
            mapperCredentialsMock = new Mock<IMapper<UserRequest, DbUserCredentials>>();
            repositoryMock = new Mock<IUserRepository>();

            command = new CreateUserCommand(validatorMock.Object, repositoryMock.Object, mapperMock.Object, mapperCredentialsMock.Object);
        }


        [Test]
        public void ShouldThrowValidationExceptionWhenValidatorReturnsFalse()
        {
            validatorMock
                .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(false);

            Assert.Throws<ValidationException>(() => command.Execute(request));
            repositoryMock.Verify(repository => repository.CreateUser(It.IsAny<DbUser>(), It.IsAny<DbUserCredentials>()), Times.Never);
        }

        [Test]
        public void ShouldThrowExceptionWhenRepositoryThrowException()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            mapperMock
                .Setup(x => x.Map(It.IsAny<UserRequest>()))
                .Returns(new DbUser());

            mapperCredentialsMock
                .Setup(x => x.Map(It.IsAny<UserRequest>()))
                .Returns(new DbUserCredentials());

            repositoryMock
                .Setup(x => x.CreateUser(It.IsAny<DbUser>(), It.IsAny<DbUserCredentials>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(request));
        }

        [Test]
        public void ShouldReturnIdFromRepositoryWhenUserCreated()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            mapperMock
                .Setup(x => x.Map(It.IsAny<UserRequest>()))
                .Returns(new DbUser());

            mapperCredentialsMock
                .Setup(x => x.Map(It.IsAny<UserRequest>()))
                .Returns(new DbUserCredentials());

            repositoryMock
                .Setup(x => x.CreateUser(It.IsAny<DbUser>(), It.IsAny<DbUserCredentials>()))
                .Returns(userId);

            Assert.AreEqual(userId, command.Execute(request));
            repositoryMock.Verify(repository => repository.CreateUser(It.IsAny<DbUser>(), It.IsAny<DbUserCredentials>()), Times.Once);
        }
    }
}

