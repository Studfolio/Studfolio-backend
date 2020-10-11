using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studfolio.VacancyService.Database.Entities
{
    public class DbVacancy
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Position { get; set; }
        [NotMapped]
        public ICollection<string> Tags { get; set; }
        public string Description { get; set; }
        public Guid CompanyId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public int SalaryFrom {get; set; }
        public int SalaryTo{ get; set; }
        public string Currency { get; set; }
        public bool Remote { get; set; }
        public string Location { get; set; }
        public bool FullTime { get; set; }
    }
}
