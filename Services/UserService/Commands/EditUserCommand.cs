using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;

namespace Studfolio.UserService.Commands
{
    public class EditUserCommand : IEditUserCommand
    {
        private readonly IValidator<UserRequest> validator;
        private readonly IUserRepository repository;
        private readonly IMapper<UserRequest, DbUser> mapper;

        public EditUserCommand(
            [FromServices] IValidator<UserRequest> validator,
            [FromServices] IUserRepository repository,
            [FromServices] IMapper<UserRequest, DbUser> mapper)
        {
            this.validator = validator;
            this.repository = repository;
            this.mapper = mapper;
        }

        public void Execute(UserRequest request)
        {
            validator.ValidateAndThrow(request);

            var user = mapper.Map(request);

            repository.EditUser(user);
        }
    }
}
