using Studfolio.AuthenticationService.Token.Interfaces;
using LT.DigitalOffice.Kernel.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace Studfolio.AuthenticationService.Token
{
    /// <inheritdoc/>
    public class JwtValidator : IJwtValidator
    {
        private readonly IJwtSigningDecodingKey _decodingKey;
        private readonly IOptions<TokenSettings> _options;
        private readonly ILogger<JwtValidator> _logger;

        public JwtValidator(
            [FromServices] IJwtSigningDecodingKey decodingKey,
            [FromServices] IOptions<TokenSettings> options,
            [FromServices] ILogger<JwtValidator> logger)
        {
            _decodingKey = decodingKey;
            _options = options;
            _logger = logger;
        }

        /// <inheritdoc/>
        public void ValidateAndThrow(string jwt)
        {
            if (string.IsNullOrEmpty(jwt))
            {
                throw new BadRequestException("Token can not be empty.");
            }

            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _options.Value.TokenIssuer,
                    ValidateAudience = true,
                    ValidAudience = _options.Value.TokenAudience,
                    ValidateLifetime = true,
                    IssuerSigningKey = _decodingKey.GetKey(),
                    ValidateIssuerSigningKey = true
                };

                new JwtSecurityTokenHandler().ValidateToken(jwt, validationParameters, out _);
            }
            catch (SecurityTokenValidationException exc)
            {
                string message = "Token failed validation.";

                _logger.LogInformation($"{message}{Environment.NewLine}{exc}");

                throw new ForbiddenException(message);
            }
            catch (Exception exc)
            {
                string message = "Token format was wrong.";

                _logger.LogInformation($"{message}{Environment.NewLine}{exc}");

                throw new BadRequestException(message);
            }
        }
    }
}