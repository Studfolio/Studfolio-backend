using LT.DigitalOffice.Kernel.Exceptions;
using Studfolio.UserService.Database;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Models;
using Studfolio.UserService.Repositories.Interfaces;
using System.Linq;

namespace Studfolio.UserService.Repositories
{
    public class UserCredentialsRepository : IUserCredentialsRepository
    {
        private readonly UserServiceDbContext dbContext;

        public UserCredentialsRepository(UserServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void ChangePassword(string email, string newPassword)
        {
            if (email == string.Empty || newPassword == string.Empty ||
                email == null || newPassword == null)
            {
                throw new BadRequestException();
            }

            DbUserCredentials userCredentials = dbContext.UserCredentials.FirstOrDefault(uc => uc.Email == email);

            if (userCredentials == null)
            {
                throw new NotFoundException("User credentials with this user login not found.");
            }

            userCredentials.PasswordHash = UserPassword.GetPasswordHash(email, userCredentials.Salt, newPassword);

            dbContext.UserCredentials.Update(userCredentials);
            dbContext.SaveChanges();
        }
    }
}
