using Studfolio.UserService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Studfolio.UserService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// Provides method for getting list of students.
    /// </summary>
    public interface IGetStudentsListCommand
    {
        /// <summary>
        /// Returns the list of user model with the specified role (student).
        /// </summary>
        /// <returns>List of students.</returns>
        List<User> Execute();
    }
}