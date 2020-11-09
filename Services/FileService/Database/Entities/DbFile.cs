using System;
using System.ComponentModel.DataAnnotations;

namespace Studfolio.FileService.Database.Entities
{
    public class DbFile
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public byte[] Content { get; set; }
        [Required]
        public string Extension { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
