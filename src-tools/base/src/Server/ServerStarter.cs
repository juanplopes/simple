using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sample.Project;
using Simple.Entities;
using Simple;
using NHibernate.Tool.hbm2ddl;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;
using Sample.Project.Environment;
using System.Threading;
using Sample.Project.Services;

namespace Sample.Project
{
    public class ServerStarter
    {
        static void Main(string[] args)
        {
            var type = typeof(SystemService).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            ThreadPool.QueueUserWorkItem(x => new Configurator().StartServer<ServerStarter>());
            Simply.Do.WaitRequests();
        }
    }
}
