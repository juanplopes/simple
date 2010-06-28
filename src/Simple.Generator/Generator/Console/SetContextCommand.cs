using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Diagnostics;

namespace Simple.Generator.Console
{
    public class SetContextCommand : IGenerator
    {
        IContextManager manager;
        public SetContextCommand(IContextManager manager)
        {
            this.manager = manager;
        }


        public string NewContext { get; set; }

        #region IGenerator Members

        public void Execute()
        {
            if (NewContext == "$")
                manager.PromptContext = null;
            else
                manager.PromptContext = NewContext;
        }

        #endregion
    }
}
