using System;
using System.ComponentModel.DataAnnotations;

namespace Imagination.Entities
{
    public class UploadEntity
    {
        [Key]
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }
        public int FileSize { get; set; }
        public string Status { get; set; }
    }
}
