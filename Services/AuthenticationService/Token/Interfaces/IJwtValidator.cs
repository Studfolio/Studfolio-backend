namespace Studfolio.AuthenticationService.Token.Interfaces
{
    /// <summary>
    /// Represents interface for user token validator.
    /// </summary>
    public interface IJwtValidator
    {
        /// <summary>
        /// Validate user token.
        /// </summary>
        /// <param name="jwt">User token.</param>
        void ValidateAndThrow(string jwt);
    }
}
