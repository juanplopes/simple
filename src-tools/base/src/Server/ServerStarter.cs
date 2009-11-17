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

namespace Sample.Project
{
    public class ServerStarter
    {
        static void Main(string[] args)
        {
            new Default(Default.Main).StartServer(Assembly.GetExecutingAssembly());
        }
    }
}
