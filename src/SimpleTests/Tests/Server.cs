using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting;
using Sample.BusinessServer.Rules;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Tcp;
using Simple.Remoting;


namespace Simple.Tests
{
    public class Server
    {
        static int Main(string[] args)
        {
            ServerHoster hoster = new ServerHoster(null);
            
            hoster.Initialize();
            hoster.AddService(typeof(EmpresaRules), "Empresa");
            hoster.AddService(typeof(FuncionarioRules), "Funcionario");
            
            
            Console.ReadLine();

            return 0;
            
        }
    }
}
