using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Entities
{
    [Table("Assessments")]
    public class Assessment_DB
    {
        [PrimaryKey, AutoIncrement]
        public int AssessmentId { get; set; }
        public int CourseId { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentType { get; set; }
        public string EndDate { get; set; }

    }
}
