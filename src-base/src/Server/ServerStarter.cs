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
using Sample.Project.Environment.Development;
using System.Reflection;

namespace Server
{
    public class ServerStarter
    {
        static void Main(string[] args)
        {
            Default.ConfigureServer();
            Simply.Do.InitServer(Assembly.GetExecutingAssembly());
        }
    }
}
