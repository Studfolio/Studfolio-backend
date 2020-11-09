using Studfolio.AuthenticationService.Models.Dto.Requests;
using Studfolio.AuthenticationService.Models.Dto.Responses;
using System.Threading.Tasks;

namespace Studfolio.AuthenticationService.Commands.Interfaces
{
    /// <summary>
    /// Represents interface for a command in command pattern.
    /// </summary>
    public interface ILoginCommand
    {
        /// <summary>
        /// Method for getting user id and jwt by email and password.
        /// </summary>
        /// <param name="request">Request model with user email and password.</param>
        /// <returns>Response model with user id and jwt</returns>
        Task<LoginResult> Execute(LoginRequest request);
    }
}