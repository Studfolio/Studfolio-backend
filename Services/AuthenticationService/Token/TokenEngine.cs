using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Studfolio.AuthenticationService.Token.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Studfolio.AuthenticationService.Token
{
    public class TokenEngine : ITokenEngine
    {
        private readonly IJwtSigningEncodingKey _signingEncodingKey;
        private readonly IOptions<TokenSettings> _tokenOptions;

        public TokenEngine(
            [FromServices] IJwtSigningEncodingKey signingEncodingKey,
            [FromServices] IOptions<TokenSettings> tokenOptions)
        {
            _signingEncodingKey = signingEncodingKey;
            _tokenOptions = tokenOptions;
        }

        /// <inheritdoc />
        public string Create(string login)
        {
            var claims = new []
            {
                new Claim(ClaimTypes.NameIdentifier, login)
            };

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Value.TokenIssuer,
                audience: _tokenOptions.Value.TokenAudience,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenOptions.Value.TokenLifetimeInMinutes),
                signingCredentials: new SigningCredentials(
                    _signingEncodingKey.GetKey(),
                    _signingEncodingKey.SigningAlgorithm));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}