
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp.RuntimeBinder;
using T4Processor;
using TGenerator.Model;
using TGenerator.TextTransformation;
using Newtonsoft.Json;
using TGenerator.App;

namespace TGenerator.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new App(args[0], args[1]);
            program.Execute();
        }        
    }
}
