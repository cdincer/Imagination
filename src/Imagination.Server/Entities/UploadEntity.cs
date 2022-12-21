using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Imagination.Entities
{
    public class UploadEntity
    {
        [Key]
        [Column(Order = 0)]
        public string FileName { get; set; } 
        [Column(Order = 1)]
        public int FileSize { get; set; }
        [Column(Order = 2)]
        public string Status { get; set; }
        [Column(Order = 3)]
        public DateTime UploadBeginDate { get; set; }
        [Column(Order = 4)]
        public DateTime UploadEndDate { get; set; }

    }
}
