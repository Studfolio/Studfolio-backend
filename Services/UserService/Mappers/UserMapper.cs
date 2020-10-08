using Studfolio.UserService.Database.Entities;
using Studfolio.UserService.Mappers.Interfaces;
using Studfolio.UserService.Models;
using Studfolio.UserService.Models.Enums;
using System;

namespace Studfolio.UserService.Mappers
{
    /// <summary>
    /// Represents mapper. Provides methods for converting an object of <see cref="DbUser"/> type into an object of <see cref="User"/> type according to some rule.
    /// </summary>
    public class UserMapper : IMapper<DbUser, User>, IMapper<UserRequest, DbUser>
    {
        public User Map(DbUser dbUser)
        {
            if (dbUser == null)
            {
                throw new ArgumentNullException(nameof(dbUser));
            }

            return new User
            {
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                MiddleName = dbUser.MiddleName,
                Role = (RoleType)dbUser.Role,
                AvatarFileId = dbUser.AvatarFileId,
                Status = dbUser.Status,
                OrganizationId = dbUser.OrganizationId,
                OrganizationName = dbUser.OrganizationName,
                Verified = dbUser.Verified,
                IsActive = dbUser.IsActive
            };
        }

        public DbUser Map(UserRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var dbUser = new DbUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Role = (int)request.Role,
                Status = request.Status,
                OrganizationId = (Guid)request.OrganizationId,
                OrganizationName = request.OrganizationName,
                IsActive = true
            };

            if (request.Id != null)
            {
                dbUser.Id = (Guid)request.Id;
            }
            else
            {
                dbUser.Id = Guid.NewGuid();
                dbUser.Verified = false;
            }

            return dbUser;
        }
    }
}