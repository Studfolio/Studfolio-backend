using FluentValidation;
using FluentValidation.Results;
using Studfolio.AuthenticationService.Models.Dto.Requests;
using Studfolio.AuthenticationService.Models.Dto.Responses;
using Studfolio.AuthenticationService.Token.Interfaces;
using Studfolio.AuthenticationService.Validation.Interfaces;
using Studfolio.Broker.Requests;
using Studfolio.Broker.Responses;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.Exceptions;
using MassTransit;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Studfolio.AuthenticationService.Commands.Interfaces;
using Studfolio.AuthenticationService.Commands;
using LT.DigitalOffice.UnitTestKernel;

namespace Studfolio.AuthenticationService.Business.UnitTests
{
    public class OperationResult<T> : IOperationResult<T>
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }
        public T Body { get; set; }
    }

    public class UserCredentialsResponse : IUserCredentialsResponse
    {
        public Guid UserId { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string UserLogin { get; set; }
    }

    public class UserLoginCommandTests
    {
        #region Variables declaration
        private Mock<ITokenEngine> tokenEngineMock;
        private Mock<ILoginValidator> loginValidatorMock;
        private Mock<ValidationResult> validationResultIsValidMock;
        private Mock<IRequestClient<IUserCredentialsRequest>> requestBrokerMock;

        private string salt;
        private ILoginCommand command;

        private LoginRequest newUserCredentials;
        private ValidationResult validationResultError;
        private UserCredentialsResponse brokerResponse;
        private OperationResult<UserCredentialsResponse> operationResult;
        #endregion

        #region Setup
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            salt = "Example_Salt1";

            loginValidatorMock = new Mock<ILoginValidator>();

            newUserCredentials = new LoginRequest
            {
                LoginData = "User_login_example",
                Password = "Example_1234"
            };

            validationResultError = new ValidationResult(
                new List<ValidationFailure>
                {
                    new ValidationFailure("error", "something", null)
                });

            validationResultIsValidMock = new Mock<ValidationResult>();

            validationResultIsValidMock
                .Setup(x => x.IsValid)
                .Returns(true);

            BrokerSetUp();

            tokenEngineMock = new Mock<ITokenEngine>();
            command = new LoginCommand(tokenEngineMock.Object, loginValidatorMock.Object, requestBrokerMock.Object);
        }

        public void BrokerSetUp()
        {
            var passwordHash = UserPassword.GetPasswordHash(
                newUserCredentials.LoginData,
                salt,
                newUserCredentials.Password);

            var responseBrokerMock = new Mock<Response<IOperationResult<IUserCredentialsResponse>>>();
            requestBrokerMock = new Mock<IRequestClient<IUserCredentialsRequest>>();

            brokerResponse = new UserCredentialsResponse
            {
                UserId = Guid.NewGuid(),
                PasswordHash = passwordHash,
                Salt = salt,
                UserLogin = newUserCredentials.LoginData
            };

            operationResult = new OperationResult<UserCredentialsResponse>
            {
                IsSuccess = true,
                Errors = new List<string>(),
                Body = brokerResponse
            };

            requestBrokerMock.Setup(
                x => x.GetResponse<IOperationResult<IUserCredentialsResponse>>(
                    It.IsAny<object>(), default, default))
                .Returns(Task.FromResult(responseBrokerMock.Object));

            responseBrokerMock
                .SetupGet(x => x.Message)
                .Returns(operationResult);
        }
        #endregion

        #region Successful test
        [Test]
        public void ShouldReturnUserIdAndJwtWhenPasswordsAndEmailHasMatch()
        {
            string JwtToken = "Example_jwt";

            var expectedLoginResponse = new LoginResult
            {
                UserId = brokerResponse.UserId,
                Token = JwtToken
            };

            loginValidatorMock
               .Setup(x => x.Validate(It.IsAny<IValidationContext>()))
               .Returns(validationResultIsValidMock.Object);

            tokenEngineMock
                .Setup(X => X.Create(newUserCredentials.LoginData))
                .Returns(JwtToken);

            SerializerAssert.AreEqual(expectedLoginResponse, command.Execute(newUserCredentials).Result);
        }
        #endregion

        #region Fail tests
        [Test]
        public void ShouldThrowExceptionWhenPasswordsHasNotMatch()
        {
            loginValidatorMock
               .Setup(x => x.Validate(It.IsAny<IValidationContext>()))
               .Returns(validationResultIsValidMock.Object);

            newUserCredentials.Password = "Example";

            Assert.ThrowsAsync<ForbiddenException>(() => command.Execute(newUserCredentials));
        }

        [Test]
        public void ShouldThrowExceptionWhenUserEmailHasNotMatchInDb()
        {
            operationResult.IsSuccess = false;
            operationResult.Errors = new List<string>() { "User email not found" };
            operationResult.Body = null;

            loginValidatorMock
               .Setup(x => x.Validate(It.IsAny<IValidationContext>()))
               .Returns(validationResultIsValidMock.Object);

            Assert.ThrowsAsync<NotFoundException>(
                () => command.Execute(newUserCredentials),
                "User email not found");
        }

        [Test]
        public void ShouldThrowExceptionWhenUserLoginInfoNotValid()
        {
            newUserCredentials.LoginData = string.Empty;

            loginValidatorMock
               .Setup(x => x.Validate(It.IsAny<IValidationContext>()))
               .Returns(validationResultError);

            Assert.ThrowsAsync<ValidationException>(() => command.Execute(newUserCredentials));
        }
        #endregion
    }
}