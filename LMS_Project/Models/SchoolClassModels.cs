using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_Project.Models
{
    public class SchoolClassModels
    {
        [Key]
        public string SchoolClassID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }

        public virtual ICollection<LessonModels> Lessons { get; set; }

        public virtual ICollection<CourseModels> Courses { get; set; }

        public SchoolClassModels()
        { }

        public SchoolClassModels(ICollection<ApplicationUser> students, ICollection<CourseModels> courses, ICollection<LessonModels> lessons)
        {
            this.Students   = students;
            this.Courses     = courses;
            this.Lessons    = lessons;
        }

        public void SetStudents(ICollection<ApplicationUser> students)
        {
            this.Students = students;
        }

        public void SetCourse(ICollection<CourseModels> courses)
        {
            this.Courses = courses;
        }
    }
}