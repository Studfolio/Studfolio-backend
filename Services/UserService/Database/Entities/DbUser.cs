using System;
using System.ComponentModel.DataAnnotations;

namespace Studfolio.UserService.Database.Entities
{
    public class DbUser
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public int Role { get; set; }
        public Guid AvatarFileId { get; set; }
        public string Status { get; set; }
        public Guid OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        [Required]
        public bool Verified { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
