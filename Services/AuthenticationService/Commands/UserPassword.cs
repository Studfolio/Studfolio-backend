using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

[assembly: InternalsVisibleTo("Studfolio.AuthenticationServiceUnitTests")]
namespace Studfolio.AuthenticationService.Commands
{
    internal static class UserPassword
    {
        private const string INTERNAL_SALT = "Studfolio.SALT3";

        internal static string GetPasswordHash(string userLogin, string salt, string userPassword)
        {
            return Encoding.UTF8.GetString(new SHA512Managed().ComputeHash(
                    Encoding.UTF8.GetBytes($"{salt}{userLogin}{userPassword}{INTERNAL_SALT}")));
        }
    }
}
