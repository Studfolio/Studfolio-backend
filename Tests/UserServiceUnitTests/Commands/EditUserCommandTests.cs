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
    public class EditUserCommandTests
    {
        private Mock<IUserRepository> repositoryMock;
        private Mock<IValidator<UserRequest>> validatorMock;
        private Mock<IMapper<UserRequest, DbUser>> mapperMock;

        private IEditUserCommand command;
        private UserRequest request;
        private DbUser dbUser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            request = new UserRequest
            {
                Id = Guid.NewGuid(),
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Email = "Example@gmail.com",
                Status = "Example",
                Password = "Example",
                IsActive = true
            };

            dbUser = new DbUser
            {
                Id = (Guid)request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Status = request.Status,
                IsActive = request.IsActive,
            };
        }

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper<UserRequest, DbUser>>();
            validatorMock = new Mock<IValidator<UserRequest>>();

            command = new EditUserCommand(validatorMock.Object, repositoryMock.Object, mapperMock.Object);
        }

        [Test]
        public void ShouldThrowExceptionWhenUserDataIsInvalid()
        {
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(false);

            Assert.Throws<ValidationException>(() => command.Execute(request));
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Never);
        }

        [Test]
        public void ShouldThrowExceptionWhenRepositoryThrowException()
        {
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(true);

            mapperMock
                .Setup(x => x.Map(It.IsAny<UserRequest>()))
                .Returns(dbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(request));
        }

        [Test]
        public void ShouldEditUserWhenUserDataIsValid()
        {
            validatorMock
                .Setup(validator => validator.Validate(It.IsAny<IValidationContext>()).IsValid)
                .Returns(true);

            mapperMock
                .Setup(x => x.Map(It.IsAny<UserRequest>()))
                .Returns(dbUser);

            repositoryMock
                .Setup(x => x.EditUser(It.IsAny<DbUser>()));

            Assert.DoesNotThrow(() => command.Execute(request));
            repositoryMock.Verify(repository => repository.EditUser(It.IsAny<DbUser>()), Times.Once);
        }
    }
}