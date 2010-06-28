using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Diagnostics;

namespace Simple.Generator.Console
{
    public class ExitCommand : IGenerator
    {
        IContextManager manager;
        public ExitCommand(IContextManager manager)
        {
            this.manager = manager;
        }

        #region IGenerator Members

        public void Execute()
        {
            if (manager.PromptContext == null)
                System.Environment.Exit(0);
            else
                manager.PromptContext = null;

        }

        #endregion
    }
}
