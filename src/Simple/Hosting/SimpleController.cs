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
using Simple.Common;

namespace Simple.Hosting
{
    public class SimpleController : ServiceBase
    {
        protected static ILog Logger = Simply.Do.Log(MethodInfo.GetCurrentMethod());

        public void Wait()
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
    }
}
