using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Models;

namespace Studfolio.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCredentialsController : ControllerBase
    {
        [HttpPost("changePassword")]
        public void ChangePassword(
            [FromServices] IChangePasswordCommand command,
            [FromBody] ChangePasswordRequest request)
        {
            command.Execute(request);
        }
    }
}
