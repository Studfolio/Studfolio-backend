using Studfolio.UserService.Models.Enums;
using System;

namespace Studfolio.UserService.Models
{
    public class UserRequest
    {
        public Guid? Id { get; set; } // For creating user only.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public RoleType Role { get; set; }
        public string Status { get; set; }
        public Guid? OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string Password { get; set; } // For creating user only.
        public string Email { get; set; } // For creating user only.
        public bool IsActive { get; set; }
    }
}
