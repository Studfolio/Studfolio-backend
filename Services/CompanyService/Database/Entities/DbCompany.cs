using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public ICollection<Guid> HRIds { get; set; }
        [NotMapped]
        public ICollection<Guid> VacanciesIds { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
