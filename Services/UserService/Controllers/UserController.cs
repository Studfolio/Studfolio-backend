using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Models;
using System;

namespace Studfolio.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("getUserInfoById")]
        public User GetUserById(
            [FromServices] IGetUserByIdCommand command,
            [FromQuery] Guid userId)
        {
            return command.Execute(userId);
        }

    }
}