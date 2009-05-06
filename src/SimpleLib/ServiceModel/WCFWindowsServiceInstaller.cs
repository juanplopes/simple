using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace Simple.ServiceModel
{
    [RunInstaller(true)]
    public partial class WCFWindowsServiceInstaller : Installer
    {
        private ServiceProcessInstaller gobjProcess = new ServiceProcessInstaller();
        private ServiceInstaller gobjService = new ServiceInstaller();

        public WCFWindowsServiceInstaller(string pstrServiceName)
            : this(pstrServiceName, pstrServiceName)
        {

        }

        public WCFWindowsServiceInstaller(string pstrServiceName, string pstrDisplayName)
        {
            gobjProcess.Account = ServiceAccount.LocalSystem;
            gobjService.ServiceName = pstrServiceName;
            gobjService.DisplayName = pstrDisplayName;
            
            Installers.Add(gobjProcess);
            Installers.Add(gobjService);
        }

        protected override void OnAfterInstall(System.Collections.IDictionary savedState)
        {
            base.OnAfterInstall(savedState);
        }
    }
}
