using Studfolio.UserService.Models;
using System;

namespace Studfolio.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for adding a new user.
    /// </summary>
    public interface ICreateUserCommand
    {
        /// <summary>
        ///  Adds a new user.
        /// </summary>
        /// <param name="request">User data.</param>
        /// <returns>Guid of added user.</returns>
        Guid Execute(UserRequest request);
    }
}
