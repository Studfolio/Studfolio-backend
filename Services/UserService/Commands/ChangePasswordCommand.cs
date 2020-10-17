using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;

namespace Studfolio.UserService.Commands
{
    public class ChangePasswordCommand : IChangePasswordCommand
    {
        private readonly IUserCredentialsRepository repository;

        public ChangePasswordCommand(
            [FromServices] IUserCredentialsRepository repository)
        {
            this.repository = repository;
        }

        public void Execute(ChangePasswordRequest request)
        {
            repository.ChangePassword(request.Email, request.NewPassword);
        }
    }
}
