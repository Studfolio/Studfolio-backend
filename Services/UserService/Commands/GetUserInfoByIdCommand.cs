using Studfolio.UserService.Commands.Interfaces;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;
using System;

namespace Studfolio.UserService.Commands
{
    /// <summary>
    /// Represents command class in command pattern. Provides method for getting user model by id.
    /// </summary>
    public class GetUserInfoByIdCommand : IGetUserByIdCommand
    {
        private readonly IUserRepository repository;
        private readonly IMapper<DbUser, User> mapper;

        /// <summary>
        /// Initialize new instance of <see cref="GetUserInfoByIdCommand"/> class with specified repository.
        /// </summary>
        /// <param name="repository">Specified repository.</param>
        /// <param name="mapper">Specified mapper that convert user model from database to user model for response.</param>
        public GetUserInfoByIdCommand(IUserRepository repository, IMapper<DbUser, User> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Return user model by specified user's id from UserServiceDb.
        /// </summary>
        /// <param name="userId">Specified user's id.</param>
        /// <returns>User model with specified id.</returns>
        public User Execute(Guid userId)
        {
            return mapper.Map(repository.GetUserInfoById(userId));
        }
    }
}