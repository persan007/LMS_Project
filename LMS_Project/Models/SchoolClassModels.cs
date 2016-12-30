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

        public ICollection<ApplicationUser> Students { get; set; }

        public ICollection<LessonModels> Lessons { get; set; }

        public virtual CourseModels Course { get; set; }

        public SchoolClassModels()
        { }

        public SchoolClassModels(ICollection<ApplicationUser> students, CourseModels course, ICollection<LessonModels> lessons)
        {
            this.Students   = students;
            this.Course     = course;
            this.Lessons    = lessons;
        }

        public void SetStudents(ICollection<ApplicationUser> students)
        {
            this.Students = students;
        }

        public void SetCourse(CourseModels course)
        {
            this.Course = course;
        }
    }
}