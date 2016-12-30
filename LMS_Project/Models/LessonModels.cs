using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LMS_Project.Models
{
    public class LessonModels
    {
        public enum Days { Monday, Tuesday, Wednesday, Thursday, Friday }

        [Key]
        public int LessonID { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public Days Day { get; set; }

        public virtual SchoolClassModels SchoolClass { get; set; }

        public virtual ScheduleModels Schedule { get; set; }


        public LessonModels()
        { }

        public LessonModels(string startTime, string endTime, Days day, SchoolClassModels schoolClass)
        {
            this.StartTime   = startTime;
            this.EndTime     = endTime;
            this.Day         = day;
            this.SchoolClass = schoolClass;
        }

        public void SetDay(Days day)
        {
            this.Day = day;
        }

        public void SetStartTime(string startTime)
        {
            this.StartTime = startTime;
        }

        public void SetEndTime(string endTime)
        {
            this.EndTime = endTime;
        }

        public void SetCourse(SchoolClassModels schoolClass)
        {
            this.SchoolClass = schoolClass;
        }
    }
}