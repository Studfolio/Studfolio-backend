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
        private IMapper<DbUser, User> mapperDbUserToUser;
        private IMapper<UserRequest, DbUser> mapperUserRequestToDbUser;

        private Guid userId;
        private DbUser dbUser;
        private UserRequest userRequest;

        [SetUp]
        public void SetUp()
        {
            mapperDbUserToUser = new UserMapper();
            mapperUserRequestToDbUser = new UserMapper();

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

            userRequest = new UserRequest
            {
                Id = userId,
                FirstName = "FirstName",
                LastName = "LastName",
                MiddleName = "MiddleName",
                Role = 0,
                Status = "Status",
                OrganizationId = Guid.NewGuid(),
                OrganizationName = "OrganizationName",
                IsActive = true
            };
        }

        #region IMapper<DbUser, User>
        [Test]
        public void ShouldThrowArgumentNullExceptionWhenDbUserIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapperDbUserToUser.Map(null));
        }

        [Test]
        public void ShouldReturnUserModelWhenMappingValidDbUser()
        {
            var result = mapperDbUserToUser.Map(dbUser);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<User>(result);
        }
        #endregion

        #region IMapper<UserRequest, DbUser>
        [Test]
        public void ShouldThrowArgumentNullExceptionWhenUserRequestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapperUserRequestToDbUser.Map(null));
        }

        [Test]
        public void ShouldReturnDbUserModelWhenMappingValid()
        {
            var result = mapperUserRequestToDbUser.Map(userRequest);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DbUser>(result);
        }
        #endregion
    }
}