using Moq;
using NUnit.Framework;
using Studfolio.FileService.Business.Interfaces;
using Studfolio.FileService.Data.Interfaces;
using System;

namespace Studfolio.FileService.Business.UnitTests
{
    public class DisableFileByIdCommandTests
    {
        private IDisableFileByIdCommand command;
        private Mock<IFileRepository> repositoryMock;

        private Guid fileId;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IFileRepository>();
            command = new DisableFileByIdCommand(repositoryMock.Object);

            fileId = Guid.NewGuid();
        }

        [Test]
        public void ShouldThrowExceptionWhenRepositoryThrowsIt()
        {
            repositoryMock.Setup(x => x.DisableFileById(It.IsAny<Guid>())).Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(fileId));
        }

        [Test]
        public void ShouldDisableFile()
        {
            repositoryMock
                .Setup(x => x.DisableFileById(It.IsAny<Guid>()));

            Assert.DoesNotThrow(() => command.Execute(fileId));
            repositoryMock.Verify(repository => repository.DisableFileById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
