using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studfolio.FileService.Database.Entities
{
    public class DbFile
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public string ContentExtension { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
