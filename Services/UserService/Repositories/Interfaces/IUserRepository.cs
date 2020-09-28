using Studfolio.UserService.Database.Entities;
using System;

namespace Studfolio.UserService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of UserService.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Returns the user with the specified id from database.
        /// </summary>
        /// <param name="userId">Specified id of user.</param>
        /// <returns>User with specified id.</returns>
        DbUser GetUserInfoById(Guid userId);
    }
}