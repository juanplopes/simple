using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Simple.Hosting
{
    [RunInstaller(true)]
    public partial class SimpleInstaller : Installer
    {
        private ServiceProcessInstaller gobjProcess = new ServiceProcessInstaller();
        private ServiceInstaller gobjService = new ServiceInstaller();

        public string ServiceName
        {
            get { return gobjService.ServiceName; }
            set { gobjService.ServiceName = value; }
        }

        public string DisplayName
        {
            get { return gobjService.DisplayName; }
            set { gobjService.DisplayName = value; }
        }

        public ServiceStartMode StartType
        {
            get { return gobjService.StartType; }
            set { gobjService.StartType = value; }
        }

        public ServiceAccount Account
        {
            get { return gobjProcess.Account; }
            set { gobjProcess.Account = value; }
        }

        public SimpleInstaller()
        {
            Account = ServiceAccount.LocalSystem;
            StartType = ServiceStartMode.Automatic;
            Installers.Add(gobjProcess);
            Installers.Add(gobjService);
        }
    }
}
