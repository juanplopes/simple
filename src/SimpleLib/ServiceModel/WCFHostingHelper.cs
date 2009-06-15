using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using Simple.Logging;
using System.Runtime.Remoting.Contexts;
using System.ServiceModel.Description;
using log4net;
using System.Reflection;

namespace Simple.ServiceModel
{
    public class WCFHostingHelper : ServiceBase, IHostingHelper
    {
        protected static ILog Logger = SimpleLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        IList<ServiceHost> glstServiceHosts = new List<ServiceHost>();
        public void Register(Type ptypServiceType)
        {
            
            Logger.Debug("Starting " + ptypServiceType.Name + "... ");
            try
            {
                ServiceHost lobjHost = GetServiceHost(ptypServiceType);
                lobjHost.UnknownMessageReceived += new EventHandler<UnknownMessageReceivedEventArgs>(lobjHost_UnknownMessageReceived);
                glstServiceHosts.Add(lobjHost);

                lobjHost.Open();
                foreach (Uri lobjUri in lobjHost.BaseAddresses)
                {
                    Logger.Info(ptypServiceType.Name + " based at: " + lobjUri);
                    foreach (ServiceEndpoint endpoint in lobjHost.Description.Endpoints)
                    {
                        Logger.Debug(">" + endpoint.Binding.GetType().Name + " at " + endpoint.Address.Uri);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error("Failed.", e);
            }
        }

        protected virtual ServiceHost GetServiceHost(Type type)
        {
            return new ServiceHost(type);
        }

        void lobjHost_UnknownMessageReceived(object sender, UnknownMessageReceivedEventArgs e)
        {
            Logger.Error("Received unknown message.");
        }

        public void Execute()
        {
            if (Environment.UserInteractive)
            {
                Logger.Info("Starting as Console Application...");
                Console.WriteLine();
                Console.WriteLine("Self-hosting application running.");
                Console.WriteLine("Type 'exit' and press <ENTER> to terminate.");
                while (Console.ReadLine().ToLower() != "exit") ;
            }
            else
            {
                Logger.Info("Starting as Windows Service...");
                Run(this);
            }
        }

        public new void Dispose()
        {
            base.Dispose();
            foreach (ServiceHost lobjHost in glstServiceHosts)
            {
                Logger.Info("Terminating " + lobjHost.Description.Name + "... ");
                try
                {
                    lobjHost.Close();
                }
                catch (Exception e)
                {
                    Logger.Error("Failed.", e);
                }
            }
        }
    }
}
