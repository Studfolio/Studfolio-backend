using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Studfolio.UserService.Mappers
{
    public class UserCredentialsMapper : IMapper<UserRequest, DbUserCredentials>
    {
        public DbUserCredentials Map(UserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new DbUserCredentials
            {
                Id = Guid.NewGuid(),
                PasswordHash = Encoding.UTF8.GetString(new SHA512Managed().ComputeHash(
                    Encoding.UTF8.GetBytes(request.Password))),
                Email = request.Email
            };
        }
    }
}
