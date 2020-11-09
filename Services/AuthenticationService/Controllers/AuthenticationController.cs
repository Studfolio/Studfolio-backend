using Microsoft.AspNetCore.Mvc;
using Studfolio.AuthenticationService.Commands.Interfaces;
using Studfolio.AuthenticationService.Models.Dto.Requests;
using Studfolio.AuthenticationService.Models.Dto.Responses;
using System.Threading.Tasks;

namespace Studfolio.AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController
    {
        [HttpPost("loginUser")]
        public async Task<LoginResult> LoginUser(
            [FromServices] ILoginCommand command,
            [FromBody] LoginRequest userCredentials)
        {
            return await command.Execute(userCredentials);
        }
    }
}