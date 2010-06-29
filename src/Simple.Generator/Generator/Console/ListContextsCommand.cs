using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Diagnostics;

namespace Simple.Generator.Console
{
    public class ListContextsCommand : ICommand
    {
        IContextManager manager;
        public ListContextsCommand(IContextManager manager)
        {
            this.manager = manager;
        }

        #region IGenerator Members

        public void Execute()
        {
            var contexts = manager.ContextNames;
            Simply.Do.Log(this).InfoFormat("Open contexts: {0}", string.Join(", ", contexts));
        }

        #endregion
    }
}
