using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Entities
{
    [Table("Terms")]
    public class Term_DB
    {
        [PrimaryKey, AutoIncrement]
        public int TermId { get; set; }
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
