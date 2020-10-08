using Microsoft.AspNetCore.Mvc;
using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Studfolio.UserService.Commands
{
    public class GetStudentsListCommand : IGetStudentsListCommand
    {
        private IUserRepository repository;
        private IMapper<DbUser, User> mapper;

        public GetStudentsListCommand(
            [FromServices] IUserRepository repository,
            [FromServices] IMapper<DbUser, User> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public List<User> Execute()
        {
            return repository.GetStudentsList().Select(student => mapper.Map(student)).ToList(); ;
        }
    }
}
