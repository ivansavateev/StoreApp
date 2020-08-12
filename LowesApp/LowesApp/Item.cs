using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;

namespace LowesApp
{
    [Table("Item")]    public class Item
    {
        [PrimaryKey, AutoIncrement, Column("_id")] public int Id { get; set; }

        public string ItemName { get; set; }

        public string Bay { get; set; }

        public string Aisle { get; set; }

        public string Location { get; set; }

        public bool IsTopStock { get; set; }
    }
}