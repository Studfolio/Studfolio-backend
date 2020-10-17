namespace Studfolio.UserService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of UserService.
    /// </summary>
    public interface IUserCredentialsRepository
    {
        /// <summary>
        /// Change password of the specified user.
        /// </summary>
        /// <param name="login">User login.</param>
        /// <param name="newPassword">New user password.</param>
        void ChangePassword(string login, string newPassword);
    }
}
