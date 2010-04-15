using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO;

namespace Sample.Project.Tools
{
    public interface IToolRunner
    {
        int Execute(CommandLineReader cmd);
    }
}
