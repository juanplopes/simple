using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Simple;
using Example.Project.Services;

namespace Example.Project.Tools.Macros
{
    public class SystemCheckMacro : ICommand
    {
        public void Execute()
        {
            var service = Simply.Do.Resolve<ISystemService>();
            foreach (var task in service.Check())
                task.Log();
        }
    }
}
