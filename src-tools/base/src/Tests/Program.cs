using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Sample.Project;
using System.Linq.Expressions;
using Simple.IO;
using Sample.Project.Environment;

namespace Tests
{

    class Program
    {
        static void Main(string[] args)
        {
            var reader = new CommandLineReader(args);
            var env = reader.Get("e", Default.Main);
        }
    }

}

