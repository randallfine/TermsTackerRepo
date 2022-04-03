using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Entities
{
    [Table("Courses")]
    public class Course_DB
    {
        [PrimaryKey, AutoIncrement]
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public string CourseTitle { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public bool HasNotifications { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public string Notes { get; set; }
    }
}
