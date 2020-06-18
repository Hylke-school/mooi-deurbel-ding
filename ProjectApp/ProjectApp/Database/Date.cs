using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectApp.Database
{
    [Table("Date")]
    class Date
    {

        [PrimaryKey, AutoIncrement, NotNull, Column("ID")]
        public int ID { get; set; }

        [NotNull, Column("date")]
        public string date { get; set; }
    }
}
