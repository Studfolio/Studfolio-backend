using NUnit.Framework;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using System;

namespace Studfolio.UserServiceUnitTests.Mappers
{
    public class UserMapperTests
    {
        private IMapper<DbUser, User> mapper;

        private const string FirstName = "Ivan";
        private const string LastName = "Dudikov";
        private const bool IsActive = true;
        private const string Status = "Hello, world!";

        private Guid userId;
        private DbUser dbUser;

        [SetUp]
        public void SetUp()
        {
            mapper = new UserMapper();

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

        #region IMapper<DbUser, User>
        [Test]
        public void ShouldThrowArgumentNullExceptionWhenDbUserIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(null));
        }

        [Test]
        public void ShouldReturnUserModelWhenMappingValidDbUser()
        {
            var resultUserModel = mapper.Map(dbUser);

            Assert.IsNotNull(resultUserModel);
            Assert.IsInstanceOf<User>(resultUserModel);
        }
        #endregion
    }
}