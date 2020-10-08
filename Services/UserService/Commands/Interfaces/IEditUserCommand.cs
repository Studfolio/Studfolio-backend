using Studfolio.UserService.Models;

namespace Studfolio.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for editing an existing user.
    /// </summary>
    public interface IEditUserCommand
    {
        /// <summary>
        ///  Editing an existing user.
        /// </summary>
        /// <param name="request">User data.</param>
        void Execute(UserRequest request);
    }
}
