using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poznamkovy_blok.Database
{
    public class Note
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date_Creation { get; set; }
        public DateTime Date_Last { get; set; }
    }
}
