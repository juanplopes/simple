using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Generator.Infra;
using System.Diagnostics;

namespace Sample.Project.Generator.Contexts
{
    public class ListContextsCommand : IGenerator
    {
        #region IGenerator Members

        public void Execute()
        {
            var contexts = Program.Manager.Names;
            Console.WriteLine("Open contexts: {0}", string.Join(", ", contexts));
        }

        #endregion
    }
}
