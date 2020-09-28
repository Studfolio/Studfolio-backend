using Studfolio.UserService.Database;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Repositories.Interfaces;
using System;
using System.Linq;

namespace Studfolio.UserService.Repositories
{
    /// <summary>
    /// Represents interface of repository. Provides method for getting user model from database.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceDbContext dbContext;

        public UserRepository(UserServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public DbUser GetUserInfoById(Guid userId)
        {
            var dbUser = dbContext.Users.FirstOrDefault(dbUser => dbUser.Id == userId);

            if (dbUser == null)
            {
                throw new Exception("User with this id not found.");
            }

            return dbUser;
        }
    }
}
