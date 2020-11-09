using Studfolio.AuthenticationService.Token.Interfaces;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System.Text;

namespace Studfolio.AuthenticationService.Token.UnitTests
{
    class SigningEncodingKeyTests
    {
        private IJwtSigningEncodingKey signingEncodingKey;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            signingEncodingKey = new SigningSymmetricKey();
        }

        [Test]
        public void SuccessfulCreateNewSymmetricSecurityKey()
        {
            string signingAlgorithm = "HS256";
            string securityKey = "qyfi0sjv1f3uiwkyflnwfvr7thpzxdxygt8t9xbhielymv20";

            var expectedKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            Assert.AreEqual(expectedKey.Key, ((SymmetricSecurityKey)signingEncodingKey.GetKey()).Key);
            Assert.AreEqual(signingAlgorithm, signingEncodingKey.SigningAlgorithm);
        }
    }
}