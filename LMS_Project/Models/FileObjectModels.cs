using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LMS_Project.Models
{
    public class FileObjectModels
    {
        [Key]
        public string ID { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserID { get; set; }

        [ForeignKey("CourseModels")]
        public string CourseID { get; set; }

        [Required]
        [StringLength(255, MinimumLength=1)]
        public string Filename { get; set; }

        [Required]
        [StringLength(100)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Data { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }

        public CourseModels CourseModels { get; set; }

        public FileObjectModels()
        {
            this.ID = Guid.NewGuid().ToString();
        }
    }
}