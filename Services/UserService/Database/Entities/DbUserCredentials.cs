using System;
using System.ComponentModel.DataAnnotations;

namespace Studfolio.UserService.Database.Entities
{
    public class DbUserCredentials
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string PasswordHash { get; set; }
    }
}
