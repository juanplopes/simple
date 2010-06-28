using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Sample.Project.Generator.Infra;
using System.Diagnostics;

namespace Sample.Project.Generator.Contexts
{
    public class SetContextCommand : IGenerator
    {
        public string NewContext { get; set; }

        #region IGenerator Members

        public void Execute()
        {
            if (NewContext == "$")
                Program.Manager.PromptContext = null;
            else
                Program.Manager.PromptContext = NewContext;
        }

        #endregion
    }
}
