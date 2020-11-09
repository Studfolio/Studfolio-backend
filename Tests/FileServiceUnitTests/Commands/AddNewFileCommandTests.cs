using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Studfolio.FileService.Business.Interfaces;
using Studfolio.FileService.Data.Interfaces;
using Studfolio.FileService.Database.Entities;
using Studfolio.FileService.Mappers.Interfaces;
using Studfolio.FileService.Models.Dto;
using System;
using System.Collections.Generic;

namespace Studfolio.FileService.Business.UnitTests
{
    public class AddNewFileCommandTests
    {
        private IAddNewFileCommand command;
        private Mock<IFileRepository> repositoryMock;
        private Mock<IValidator<FileCreateRequest>> validatorMock;
        private Mock<IMapper<FileCreateRequest, DbFile>> mapperMock;

        private DbFile newFile;
        private FileCreateRequest fileRequest;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IFileRepository>();
            validatorMock = new Mock<IValidator<FileCreateRequest>>();
            mapperMock = new Mock<IMapper<FileCreateRequest, DbFile>>();

            newFile = new DbFile
            {
                Id = Guid.NewGuid(),
                Content = Convert.FromBase64String("RGlnaXRhbCBPZmA5Y2U="),
                Extension = ".txt",
                IsActive = true,
                Name = "DigitalOfficeTestFile",
            };

            fileRequest = new FileCreateRequest
            {
                Content = "RGlnaXRhbCBPZmA5Y2U=",
                Extension = ".txt",
                Name = "DigitalOfficeTestFile"
            };

            mapperMock
                .Setup(f => f.Map(It.IsAny<FileCreateRequest>()))
                .Returns(newFile);

            command = new AddNewFileCommand(repositoryMock.Object, validatorMock.Object, mapperMock.Object);
        }

        [Test]
        public void ShouldAddNewFile()
        {
            var fileId = Guid.NewGuid();

            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            repositoryMock
                .Setup(x => x.AddNewFile(It.IsAny<DbFile>()))
                .Returns(fileId);

            Assert.AreEqual(fileId, command.Execute(fileRequest));
            validatorMock.Verify(v => v.Validate(It.IsAny<IValidationContext>()), Times.Once);
            repositoryMock.Verify(r => r.AddNewFile(newFile), Times.Once);
            mapperMock.Verify(m => m.Map(fileRequest), Times.Once);
        }

        [Test]
        public void ShouldThrowExceptionWhenValidatorThrowsException()
        {
            validatorMock
                .Setup(x => x.Validate(It.IsAny<IValidationContext>()))
                .Returns(new ValidationResult(
                    new List<ValidationFailure>
                    {
                        new ValidationFailure("test", "something", null)
                    }));

            Assert.Throws<ValidationException>(() => command.Execute(fileRequest));
            repositoryMock.Verify(r => r.AddNewFile(newFile), Times.Never);
            mapperMock.Verify(m => m.Map(fileRequest), Times.Never);
        }

        [Test]
        public void ShouldThrowExceptionWhenRepositoryThrowsException()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            repositoryMock
                .Setup(x => x.AddNewFile(It.IsAny<DbFile>()))
                .Throws(new Exception());

            Assert.Throws<Exception>(() => command.Execute(fileRequest), "GUID duplicated error");
            validatorMock.Verify(v => v.Validate(It.IsAny<IValidationContext>()), Times.Once);
            repositoryMock.Verify(r => r.AddNewFile(newFile), Times.Once);
            mapperMock.Verify(m => m.Map(fileRequest), Times.Once);
        }

        [Test]
        public void ShouldThrowExceptionWhenFileRequestIsNull()
        {
            validatorMock
                 .Setup(x => x.Validate(It.IsAny<IValidationContext>()).IsValid)
                 .Returns(true);

            mapperMock
                 .Setup(x => x.Map(It.IsAny<FileCreateRequest>()))
                 .Throws(new NullReferenceException());

            fileRequest = null;

            Assert.Throws<NullReferenceException>(() => command.Execute(fileRequest), "Request is null");
        }
      
        public void ShouldThrowNullReferenceExceptionWhenMapperThrowsIt()
        {
            mapperMock
                .Setup(x => x.Map(It.IsAny<FileCreateRequest>()))
                .Throws(new ArgumentNullException());

            Assert.Throws<ArgumentNullException>(() => command.Execute(fileRequest));
        }
    }
}