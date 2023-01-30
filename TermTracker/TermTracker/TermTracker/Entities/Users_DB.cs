using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TermTracker.Entities
{
    [Table("Users")]
    public class Users_DB
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
