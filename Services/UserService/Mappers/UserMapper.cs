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
    public class UserMapper : IMapper<DbUser, User>
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
    }
}