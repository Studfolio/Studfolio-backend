using Studfolio.UserService.Database.Entities;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// Returns the list of users with the specified role (student) from database.
        /// </summary>
        /// <returns>List of students.</returns>
        List<DbUser> GetStudentsList();

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="user">User to add.</param>
        /// <param name="userCredentials">User credentials to add.</param>
        /// <returns>Guid of added user.</returns>
        Guid CreateUser(DbUser user, DbUserCredentials userCredentials);

        /// <summary>
        /// Edits an existing user.
        /// </summary>
        /// <param name="user">User to edit.</param>
        void EditUser(DbUser user);
    }
}