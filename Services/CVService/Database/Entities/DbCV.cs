using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studfolio.CVService.Database.Entities
{
    public class DbCV
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        public string Position { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string Description { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public Guid FileFileId { get; set; }
    }
}