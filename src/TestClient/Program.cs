using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Rules;
using Simple.Tests.Contracts;
using Simple.Filters;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Channels.Tcp;
using Simple.Remoting;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientFactory client = new ClientFactory(null);
            IEmpresaRules server1 = client.Create<IEmpresaRules>("Empresa");
            IFuncionarioRules server2 = client.Create<IFuncionarioRules>("Funcionario");
            IEmpresaFuncionarioRules server3 = client.Create<IEmpresaFuncionarioRules>("EmpresaFuncionario");

            server1.Load(1);
            server2.Load(1);

            var q = server2.ListByFilter(BooleanExpression.True, null);

        }
    }
}

