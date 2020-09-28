using Studfolio.UserService.Models;
using System;

namespace Studfolio.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for getting user model by id.
    /// </summary>
    public interface IGetUserByIdCommand
    {
        /// <summary>
        /// Returns the user model with the specified id.
        /// </summary>
        /// <param name="userId">Specified id of user.</param>
        /// <returns>User model with specified id.</returns>
        User Execute(Guid userId);
    }
}
