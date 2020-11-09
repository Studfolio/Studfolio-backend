using Studfolio.AuthenticationService.Token.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;

namespace Studfolio.AuthenticationService.Broker.Consumers
{
    public class JwtConsumer : IConsumer<ICheckTokenRequest>
    {
        private readonly IJwtValidator _jwtValidator;

        public JwtConsumer([FromServices] IJwtValidator jwtValidator)
        {
            _jwtValidator = jwtValidator;
        }

        public async Task Consume(ConsumeContext<ICheckTokenRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetValidationResultJwt, context.Message);

            await context.RespondAsync<IOperationResult<bool>>(response);
        }

        private bool GetValidationResultJwt(ICheckTokenRequest request)
        {
            _jwtValidator.ValidateAndThrow(request.Token);

            return true;
        }
    }
}