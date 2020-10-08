using NUnit.Framework;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using System;

namespace Studfolio.UserServiceUnitTests.Mappers
{
    public class UserCredentialsMapperTests
    {
        private IMapper<UserRequest, DbUserCredentials> mapper;

        private Guid userId;
        private UserRequest userRequest;

        [SetUp]
        public void SetUp()
        {
            mapper = new UserCredentialsMapper();

            userId = Guid.NewGuid();
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
                IsActive = true,
                Email = "123@mail.ru",
                Password = "12345678"
            };
        }

        #region IMapper<UserRequest, DbUser>
        [Test]
        public void ShouldThrowArgumentNullExceptionWhenUserRequestIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => mapper.Map(null));
        }

        [Test]
        public void ShouldReturnDbUserCredentialsWhenMappingUserRequest()
        {
            var result = mapper.Map(userRequest);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DbUserCredentials>(result);
        }
        #endregion
    }
}
