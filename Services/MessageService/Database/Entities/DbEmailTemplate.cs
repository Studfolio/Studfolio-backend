using System;
using System.ComponentModel.DataAnnotations;

namespace Studfolio.MessageService.Database.Entities
{
    public class DbEmailTemplate
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
