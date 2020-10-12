using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studfolio.PortfolioService.Database.Entities
{
    public class DbPortfolio
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Specialization { get; set; }
        [NotMapped]
        public ICollection<string> Tags { get; set; }
        [NotMapped]
        public ICollection<Guid> FilesIds { get; set; }
        [NotMapped]
        public ICollection<Guid> CertificatesFilesIds { get; set; }
        [NotMapped]
        public ICollection<string> Links { get; set; }
        public Guid CVId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public bool Public { get; set; }
    }
}
