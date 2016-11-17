using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace XFEmbeddSQLiteFile
{
    [Table("Customer")]
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
