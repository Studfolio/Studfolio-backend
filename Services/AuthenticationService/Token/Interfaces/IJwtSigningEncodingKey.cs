using Microsoft.IdentityModel.Tokens;

namespace Studfolio.AuthenticationService.Token.Interfaces
{
    /// <summary>
    /// Represents interface for create encoding key jwt.
    /// </summary>
    public interface IJwtSigningEncodingKey
    {
        ///<value>Type of algorithm encoding(HS256).</value>
        string SigningAlgorithm { get; }

        /// <summary>
        /// Method for getting encoding key jwt.
        /// </summary>
        /// <returns>Return key to create the signature(private key).</returns>
        SecurityKey GetKey();
    }
}