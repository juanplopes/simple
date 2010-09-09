using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Example.Project;
using Simple.Entities;
using Simple;
using NHibernate.Tool.hbm2ddl;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;
using Example.Project.Config;
using System.Threading;
using Example.Project.Services;
using Example.Project.Tools;

namespace Example.Project
{
    public class ServerStarter
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive || (args != null && args.Length > 0))
                ToolsStarter.Main(args);
            else
            {
                ThreadPool.QueueUserWorkItem(x => new Configurator().StartServer<ServerStarter>());
                Simply.Do.WaitRequests();
            }
        }
    }
}
