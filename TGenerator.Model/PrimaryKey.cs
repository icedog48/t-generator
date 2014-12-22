using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGenerator.Model
{
    public class PrimaryKey
    {
        public string ColumnName { get; set; }

        public bool IsAutoIncremment { get; set; }
    }
}
