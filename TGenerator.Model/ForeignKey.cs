using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TGenerator.Model
{
    public class ForeignKey
    {
        public string Name { get; set; }

        public string PropertyName { get; set; }

        public string ColumnName { get; set; }

        public string ColumnReference { get; set; }

        public string TableReference { get; set; }
    }
}
