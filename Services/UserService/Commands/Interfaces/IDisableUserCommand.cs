using System;

namespace Studfolio.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for getting result of operation to disabling user.
    /// </summary>
    public interface IDisableUserCommand
    {
        /// <summary>
        /// Disable existing user.
        /// </summary>
        /// <param name="userId">Specified id.</param>
        void Execute(Guid userId);
    }
}
