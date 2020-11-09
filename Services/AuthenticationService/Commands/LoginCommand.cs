using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Kernel.Exceptions;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Studfolio.AuthenticationService.Commands.Interfaces;
using Studfolio.AuthenticationService.Models.Dto.Requests;
using Studfolio.AuthenticationService.Models.Dto.Responses;
using Studfolio.AuthenticationService.Token.Interfaces;
using Studfolio.AuthenticationService.Validation.Interfaces;
using Studfolio.Broker.Requests;
using Studfolio.Broker.Responses;
using System.Threading.Tasks;

namespace Studfolio.AuthenticationService.Commands
{
    public class LoginCommand : ILoginCommand
    {
        private readonly ITokenEngine tokenEngine;
        private readonly ILoginValidator validator;
        private readonly IRequestClient<IUserCredentialsRequest> requestClient;

        public LoginCommand(
            [FromServices] ITokenEngine tokenEngine,
            [FromServices] ILoginValidator validator,
            [FromServices] IRequestClient<IUserCredentialsRequest> requestClient)
        {
            this.tokenEngine = tokenEngine;
            this.validator = validator;
            this.requestClient = requestClient;
        }

        public async Task<LoginResult> Execute(LoginRequest request)
        {
            validator.ValidateAndThrowCustom(request);

            var savedUserCredentials = await GetUserCredentials(request.LoginData);

            VerifyPasswordHash(savedUserCredentials, request.Password, savedUserCredentials.UserLogin);

            var result = new LoginResult
            {
                UserId = savedUserCredentials.UserId,
                Token = tokenEngine.Create(request.LoginData)
            };

            return result;
        }

        private async Task<IUserCredentialsResponse> GetUserCredentials(string loginData)
        {
            var brokerResponse = await requestClient.GetResponse<IOperationResult<IUserCredentialsResponse>>(
                IUserCredentialsRequest.CreateObj(loginData));

            if (!brokerResponse.Message.IsSuccess)
            {
                throw new NotFoundException(brokerResponse.Message.Errors);
            }

            return brokerResponse.Message.Body;
        }

        private void VerifyPasswordHash(IUserCredentialsResponse savedUserCredentials, string requestPassword, string userLogin)
        {
            string requestPasswordHash = UserPassword.GetPasswordHash(
                userLogin,
                savedUserCredentials.Salt,
                requestPassword);

            if (!string.Equals(savedUserCredentials.PasswordHash, requestPasswordHash))
            {
                throw new ForbiddenException("Wrong user credentials.");
            }
        }
    }
}