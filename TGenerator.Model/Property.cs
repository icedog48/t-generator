using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGenerator.Model
{
    public enum TypeOf
    {
        OBJECT, LIST
    }

    public class Property
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public TypeOf? TypeOf { get; set; }
    }
}
