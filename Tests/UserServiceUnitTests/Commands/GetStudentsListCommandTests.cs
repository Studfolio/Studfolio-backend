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
using System.Collections.Generic;

namespace Studfolio.UserServiceUnitTests.Commands
{
    public class GetStudentsListCommandTests
    {
        private Mock<IUserRepository> repositoryMock;
        private Mock<IMapper<DbUser, User>> mapperMock;
        private IGetStudentsListCommand command;
        private List<DbUser> dbStudentsList;
        private DbUser dbUser;
        private User user;

        [SetUp]
        public void Setup()
        {
            mapperMock = new Mock<IMapper<DbUser, User>>();
            repositoryMock = new Mock<IUserRepository>();
            command = new GetStudentsListCommand(repositoryMock.Object, mapperMock.Object);

            dbUser = new DbUser
            {
                Id = Guid.NewGuid(),
                FirstName = "Alex",
                Role = 2
            };
            user = new User
            {
                FirstName = "Alex",
                Role = (RoleType)dbUser.Role
            };
            dbStudentsList = new List<DbUser> { dbUser };
        }
        [Test]
        public void ShouldGetRightsList()
        {
            repositoryMock.Setup(repository => repository.GetStudentsList())
                .Returns(dbStudentsList)
                .Verifiable();

            mapperMock.Setup(mapper => mapper.Map(dbUser))
                .Returns(user)
                .Verifiable();

            Assert.That(command.Execute(), Is.EquivalentTo(new List<User> { user }));
            repositoryMock.Verify();
            mapperMock.Verify();
        }
        [Test]
        public void ShouldThrowExceptionWhenRepositoryThrowsException()
        {
            repositoryMock.Setup(repository => repository.GetStudentsList())
                .Throws(new Exception());

            Assert.That(() => command.Execute(), Throws.TypeOf<Exception>());
        }
        [Test]
        public void ShouldThrowExceptionWhenMapperThrowsException()
        {
            repositoryMock.Setup(repository => repository.GetStudentsList())
                .Returns(dbStudentsList)
                .Verifiable();

            mapperMock.Setup(mapper => mapper.Map(It.IsAny<DbUser>()))
                .Throws(new Exception())
                .Verifiable();

            Assert.That(() => command.Execute(), Throws.TypeOf<Exception>());
            repositoryMock.Verify();
            mapperMock.Verify();
        }
    }
}
