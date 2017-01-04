using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_Project.Models
{
    public class ScheduleModels
    {
        [Key]
        public string ScheduleID { get; set; }

        public ICollection<LessonModels> Lessons { get; set; }

        public virtual SchoolClassModels SchoolClass { get; set; }

        public TimeSpan Period { get; set; }

        public ScheduleModels()
        {}

        public ScheduleModels(ICollection<LessonModels> lessons, SchoolClassModels schoolClass, TimeSpan period)
        {
            this.Lessons        = lessons;
            this.SchoolClass    = schoolClass;
            this.Period         = period;
        }

        public void SetLessons(ICollection<LessonModels> lessons)
        {
            this.Lessons = lessons;
        }

        public void SetSchoolClass(SchoolClassModels schoolClass)
        {
            this.SchoolClass = schoolClass;
        }

        public void SetPeriod(TimeSpan period)
        {
            this.Period = period;
        }
    }
}