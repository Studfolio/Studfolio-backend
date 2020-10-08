using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserService.Commands
{
    public class CreateUserCommand : ICreateUserCommand
    {
        private readonly IValidator<UserRequest> validator;
        private readonly IUserRepository repository;
        private readonly IMapper<UserRequest, DbUser> mapper;
        private readonly IMapper<UserRequest, DbUserCredentials> mapperCredentials;

        public CreateUserCommand(
            [FromServices] IValidator<UserRequest> validator,
            [FromServices] IUserRepository repository,
            [FromServices] IMapper<UserRequest, DbUser> mapper,
            [FromServices] IMapper<UserRequest, DbUserCredentials> mapperCredentials)
        {
            this.validator = validator;
            this.repository = repository;
            this.mapper = mapper;
            this.mapperCredentials = mapperCredentials;
        }

        public Guid Execute(UserRequest request)
        {
            validator.ValidateAndThrow(request);

            var user = mapper.Map(request);
            var userCredentials = mapperCredentials.Map(request);

            return repository.CreateUser(user, userCredentials);
        }
    }
}
