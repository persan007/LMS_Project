using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;



namespace LMS_Project.Models
{
    public class FileObjectUserModels
    {
        [Key]
        public string FileObjectUserID { get; set; }
        
        [ForeignKey("ApplicationUser")]
        public virtual string UserID { get; set; }

        [ForeignKey("FileObjectModels")]
        public string FileObjectID { get; set; }

        public bool Shared { get; set; }

        public DateTime UploadedTime { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public FileObjectModels FileObjectModels { get; set; }

        public FileObjectUserModels()
        {
        }
    }
}