using System;
using System.ComponentModel.DataAnnotations;

namespace Studfolio.MessageService.Database.Entities
{
    public class DbEmail
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        public Guid SenderId { get; set; }
        [Required]
        public string Receiver { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
}
