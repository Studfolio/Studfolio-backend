using LT.DigitalOffice.Kernel.UnitTestLibrary;
using Moq;
using NUnit.Framework;
using Studfolio.UserService.Commands;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Models.Enums;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserServiceUnitTests.Commands
{
    public class GetUserInfoByIdCommandTests
    {
        private IGetUserByIdCommand command;
        private Mock<IUserRepository> repositoryMock;
        private Mock<IMapper<DbUser, User>> mapperMock;

        private Guid userId;
        private DbUser dbUser;

        [SetUp]
        public void SetUp()
        {
            repositoryMock = new Mock<IUserRepository>();
            mapperMock = new Mock<IMapper<DbUser, User>>();
            command = new GetUserInfoByIdCommand(repositoryMock.Object, mapperMock.Object);

            userId = Guid.NewGuid();
            dbUser = new DbUser
            {
                Id = userId,
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
        }

        [Test]
        public void ShouldReturnModelOfUser()
        {
            var expectedUser = new User
            {
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName",
                Role = (RoleType)dbUser.Role,
                AvatarFileId = dbUser.AvatarFileId,
                Status = "Status",
                OrganizationId = dbUser.OrganizationId,
                OrganizationName = "OrganizationName",
                Verified = true,
                IsActive = true
            };

            repositoryMock.Setup(repository => repository.GetUserInfoById(userId)).Returns(dbUser).Verifiable();
            mapperMock.Setup(mapper => mapper.Map(dbUser)).Returns(expectedUser).Verifiable();

            SerializerAssert.AreEqual(expectedUser, command.Execute(userId));
            repositoryMock.Verify();
            mapperMock.Verify();
        }

        [Test]
        public void ShouldThrowExceptionWhenMapperThrowsException()
        {
            repositoryMock.Setup(repository => repository.GetUserInfoById(It.IsAny<Guid>())).Returns(dbUser).Verifiable();
            mapperMock.Setup(mapper => mapper.Map(It.IsAny<DbUser>())).Throws<Exception>().Verifiable();

            Assert.Throws<Exception>(() => command.Execute(It.IsAny<Guid>()));
            mapperMock.Verify();
            repositoryMock.Verify();
        }

        [Test]
        public void ShouldThrowExceptionWhenRepositoryThrowsException()
        {
            repositoryMock.Setup(repository => repository.GetUserInfoById(userId)).Throws<Exception>().Verifiable();
            mapperMock.Setup(mapper => mapper.Map(dbUser)).Returns(new User());

            Assert.Throws<Exception>(() => command.Execute(userId));
            repositoryMock.Verify();
            mapperMock.Verify(mapper => mapper.Map(It.IsAny<DbUser>()), Times.Never);
        }
    }
}
