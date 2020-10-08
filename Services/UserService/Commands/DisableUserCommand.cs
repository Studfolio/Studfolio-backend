using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserService.Commands
{
    public class DisableUserCommand : IDisableUserCommand
    {
        private readonly IUserRepository repository;

        public DisableUserCommand(
            [FromServices] IUserRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(Guid userId)
        {
            DbUser editedUser = repository.GetUserInfoById(userId);

            editedUser.IsActive = false;

            repository.EditUser(editedUser);
        }
    }
}
