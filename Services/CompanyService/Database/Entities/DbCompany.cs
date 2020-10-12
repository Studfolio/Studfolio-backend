using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Studfolio.CompanyService.Database.Entities
{
    public class DbCompany
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public ICollection<Guid> HRIds { get; set; }
        public ICollection<Guid> VacanciesIds { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
