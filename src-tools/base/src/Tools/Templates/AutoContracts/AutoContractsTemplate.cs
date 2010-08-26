using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Reflection;
using Simple;
using Simple.Services;
using Simple.Reflection;
using Simple.NVelocity;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics.SymbolStore;
using Simple.IO.Serialization;
using Microsoft.Build.Utilities;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Simple.Generator.Misc;
using System.Threading;

namespace Example.Project.Tools.Templates.AutoContracts
{

    public class AutoContractsTemplate : ICommand
    {
        #region ICommand Members

        public void Execute()
        {
            Interfaces.HideInterfaces();
            var psi = new ProcessStartInfo("msbuild.exe", Path.GetFileName(Options.Do.SolutionFile));
            psi.WorkingDirectory = Path.GetDirectoryName(Options.Do.SolutionFile);
            psi.UseShellExecute = false;
            var p = Process.Start(psi);
            p.WaitForExit();
        }
       
      

        public void Verify()
        {
            if (Interfaces.ShowInterfaces() > 0)
            {
                new AutoServiceRunner().Run();
                new AutoDomainRunner().Run();
            }
        }

       

        #endregion
    }
}
