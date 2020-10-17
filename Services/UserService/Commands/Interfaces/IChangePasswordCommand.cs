using Studfolio.UserService.Models;

namespace Studfolio.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for changing user password.
    /// </summary>
    public interface IChangePasswordCommand
    {
        /// <summary>
        /// Change password of the user with specified login.
        /// </summary>
        /// <param name="request">User data, includes user login and new password.</param>
        void Execute(ChangePasswordRequest request);
    }
}
