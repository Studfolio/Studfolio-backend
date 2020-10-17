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

            string salt = $"{ Guid.NewGuid() }{ Guid.NewGuid() }";

            return new DbUserCredentials
            {
                Id = Guid.NewGuid(),
                Salt = salt,
                PasswordHash = UserPassword.GetPasswordHash(request.Email, salt, request.Password),
                Email = request.Email
            };
        }
    }
}
