using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Studfolio.FileService.Database.Entities
{
    public class DBFile
    {
        [Key]
        public Guid Id { get; set; }

        public byte[] Content { get; set; }

        public string ContentExtension { get; set; }

        public string Name { get; set; }

        public bool isActive { get; set; }
    }
}
