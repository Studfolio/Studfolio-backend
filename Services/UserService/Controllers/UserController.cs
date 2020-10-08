using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Models;
using System;
using System.Collections.Generic;

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

        [HttpGet("getStudentsList")]
        public List<User> GetStudentsList(
            [FromServices] IGetStudentsListCommand command)
        {
            return command.Execute();
        }

        [HttpPost("registerUser")]
        public Guid CreateUser(
            [FromServices] ICreateUserCommand command,
            [FromBody] UserRequest request)
        {
            return command.Execute(request);
        }

        [HttpPost("editUser")]
        public void EditUser(
            [FromServices] IEditUserCommand command,
            [FromBody] UserRequest request)
        {
            command.Execute(request);
        }

        [HttpDelete("disableUser")]
        public void DisableUserById(
            [FromServices] IDisableUserCommand command,
            [FromQuery] Guid userId)
        {
            command.Execute(userId);
        }
    }
}