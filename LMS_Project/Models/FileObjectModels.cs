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
    public class FileObjectModels
    {
        [Key]
        public string FileObjectID { get; set; }

        //[ForeignKey("ApplicationUser")]
        //public string UserID { get; set; }

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
            this.FileObjectID = Guid.NewGuid().ToString();
            this.ApplicationUser = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());
        }
    }
}