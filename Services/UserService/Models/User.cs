using Studfolio.UserService.Models.Enums;
using System;

namespace Studfolio.UserService.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public RoleType Role { get; set; }
        public Guid AvatarFileId { get; set; }
        public string Status { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public bool Verified { get; set; }
        public bool IsActive { get; set; }

    }
}
