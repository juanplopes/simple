using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using BasicLibrary.Logging;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel.Description;

namespace BasicLibrary.ServiceModel
{
    public class WCFHostingHelper : ServiceBase, IHostingHelper
    {
        IList<ServiceHost> glstServiceHosts = new List<ServiceHost>();
        public void Register(Type ptypServiceType)
        {
            MainLogger.Default.Debug("Starting " + ptypServiceType.Name + "... ");
            try
            {
                ServiceHost lobjHost = GetServiceHost(ptypServiceType);
                lobjHost.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(lobjHost_UnknownMessageReceived);
                glstServiceHosts.Add(lobjHost);

                lobjHost.Open();
                foreach (Uri lobjUri in lobjHost.BaseAddresses)
                {
                    MainLogger.Default.Info(ptypServiceType.Name + " based at: " + lobjUri);
                    foreach (ServiceEndpoint endpoint in lobjHost.Description.Endpoints)
                    {
                        MainLogger.Default.Debug(">" + endpoint.Binding.GetType().Name + " at " + endpoint.Address.Uri);
                    }
                }
            }
            catch (Exception e)
            {
                MainLogger.Default.Error("Failed.", e);
            }
        }

        protected virtual ServiceHost GetServiceHost(Type type)
        {
            return new ServiceHost(type);
        }

        void lobjHost_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            MainLogger.Default.Error("Received unknown message.");
        }

        public void Execute()
        {
            if (Environment.UserInteractive)
            {
                Console.WriteLine();
                Console.WriteLine("Self-hosting application running.");
                Console.WriteLine("Type 'exit' and press <ENTER> to terminate.");
                while (Console.ReadLine().ToLower() != "exit") ;
            }
            else
            {
                Run(this);
            }
        }

        public new void Dispose()
        {
            base.Dispose();
            foreach (ServiceHost lobjHost in glstServiceHosts)
            {
                MainLogger.Default.Info("Terminating " + lobjHost.Description.Name + "... ");
                try
                {
                    lobjHost.Close();
                }
                catch (Exception e)
                {
                    MainLogger.Default.Error("Failed.", e);
                }
            }
        }
    }
}
