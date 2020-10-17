using LT.DigitalOffice.Kernel.Exceptions;
using Moq;
using NUnit.Framework;
using Studfolio.UserService.Commands;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;

namespace Studfolio.UserServiceUnitTests.Commands
{
    public class ChangePasswordCommandTests
    {
        private Mock<IUserCredentialsRepository> repositoryMock;
        private IChangePasswordCommand command;

        private ChangePasswordRequest request;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IUserCredentialsRepository>();

            command = new ChangePasswordCommand(repositoryMock.Object);

            request = new ChangePasswordRequest
            {
                Email = "email@mail.ru",
                NewPassword = "NewPassword"
            };
        }

        [Test]
        public void ShouldThrowBadRequestExceptionWhenRequestFieldsAreNull()
        {
            repositoryMock
                .Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<BadRequestException>();

            Assert.Throws<BadRequestException>(() => command.Execute(request));
        }

        [Test]
        public void ShouldThrowNotFoundExceptionWhenLoginWasNotFound()
        {
            repositoryMock
                .Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<NotFoundException>();

            Assert.Throws<NotFoundException>(() => command.Execute(request));
        }

        [Test]
        public void ShouldChangePassword()
        {
            repositoryMock
                .Setup(x => x.ChangePassword(It.IsAny<string>(), It.IsAny<string>()));

            Assert.DoesNotThrow(() => command.Execute(request));
            repositoryMock.Verify(repository => repository.ChangePassword(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
