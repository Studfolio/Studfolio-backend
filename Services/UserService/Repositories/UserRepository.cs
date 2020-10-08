using Studfolio.UserService.Database;
using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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
                throw new Exception("User with this id was not found.");
            }

            return dbUser;
        }

        public List<DbUser> GetStudentsList()
        {
            var students = dbContext.Users.Where(dbUser => dbUser.Role == 2).ToList();

            if (students == null)
            {
                throw new Exception("Students were not found.");
            }

            return students;
        }

        public Guid CreateUser(DbUser user, DbUserCredentials userCredentials)
        {
            if (dbContext.UserCredentials.Any(credentials => userCredentials.Email == credentials.Email))
            {
                throw new Exception("Email is already taken.");
            }

            userCredentials.UserId = user.Id;

            dbContext.Users.Add(user);
            dbContext.UserCredentials.Add(userCredentials);
            dbContext.SaveChanges();

            return user.Id;
        }

        public void EditUser(DbUser user)
        {
            if (!dbContext.Users.Any(users => user.Id == users.Id))
            {
                throw new Exception("User was not found.");
            }

            dbContext.Users.Update(user);
            dbContext.SaveChanges();
        }
    }
}
