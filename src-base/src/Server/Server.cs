using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Conspirarte;
using Conspirarte.Domain;
using Simple.Entities;
using Simple;
using NHibernate.Tool.hbm2ddl;
using System.Xml.Linq;
using System.Xml;
using Conspirarte.Environment.Development;
using System.Reflection;

namespace Server
{
    public class Server
    {
        static void Main(string[] args)
        {
            Default.ConfigureServer()
                .HostAssembly(Assembly.GetExecutingAssembly());


            Console.ReadLine();
        }
    }
}
