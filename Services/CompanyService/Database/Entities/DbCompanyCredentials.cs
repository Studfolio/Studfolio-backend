using System;
using System.ComponentModel.DataAnnotations;

namespace Studfolio.CompanyService.Database.Entities
{
    public class DbCompanyCredentials
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
