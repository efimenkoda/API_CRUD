using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_CRUD.Models
{
    public abstract class Sheet
    {
        public int Id { get; set; }
        public string ColumnName { get; set; }
        public string Value { get; set; }
        public DateTime DateTime { get; set; }
    }
}
