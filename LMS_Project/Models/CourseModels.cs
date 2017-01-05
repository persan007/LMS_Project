using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace LMS_Project.Models
{
    public class CourseModels
    {
        [Key]
        public string CourseID { get; set; }

        public string Subject { get; set; }

        public string CourseLevel { get; set; }

        public DateTime SubjectStartsAt { get; set; }

        public DateTime SubjectEndsAt { get; set; }

        public virtual ICollection<ApplicationUser> Teachers { get; set; }

        public virtual ICollection<SchoolClassModels> SchoolClasses { get; set; }

        public virtual ConsoleColor DisplayColor { get; set; }

        public CourseModels()
        {
            this.CourseID = Guid.NewGuid().ToString();
        }
    }
}
