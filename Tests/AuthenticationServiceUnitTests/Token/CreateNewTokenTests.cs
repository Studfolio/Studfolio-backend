using Studfolio.AuthenticationService.Token.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using NUnit.Framework;
using System.Text;

namespace Studfolio.AuthenticationService.Token.UnitTests
{
    public class CreateNewTokenTests
    {
        private Mock<IJwtSigningEncodingKey> signingEncodingKey;
        private SymmetricSecurityKey expectedKey;
        private TokenEngine tokenEngine;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            string securityKey = "qyfi0sjv1f3uiwkyflnwfvr7thpzxdxygt8t9xbhielymv20";

            signingEncodingKey = new Mock<IJwtSigningEncodingKey>();

            var tokenOptions = Options.Create(new TokenSettings
            {
                TokenAudience = "AuthClient",
                TokenIssuer = "AuthClient",
                TokenLifetimeInMinutes = 5
            });

            expectedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            tokenEngine = new TokenEngine(signingEncodingKey.Object, tokenOptions);
        }

        [Test]
        public void SuccessfulCreateNewToken()
        {
            string emailUser = "digitalOffice@lanit-tercom.com";
            string signingAlgorithm = "HS256";

            signingEncodingKey
                .Setup(x => x.GetKey())
                .Returns(expectedKey);

            signingEncodingKey
                .SetupGet(x => x.SigningAlgorithm)
                .Returns(signingAlgorithm);

            var newJwt = tokenEngine.Create(emailUser);

            Assert.IsNotEmpty(newJwt);
        }
    }
}