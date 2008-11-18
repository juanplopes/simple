using Sample.BusinessInterface;
using SimpleLibrary.Rules;
using SimpleLibrary.Filters;
using System.Collections.Generic;
using Sample.BusinessInterface.Domain;
using System.Threading;
using log4net;
using BasicLibrary.Logging;
using log4net.DateFormatter;
using log4net.Appender;
using System.Diagnostics;
using System;
using System.Globalization;

namespace Sample.UserInterface2
{
    interface ITest
    {
        void Test();
    }

    class Test : ITest
    {
        void ITest.Test()
        {
            throw new NotImplementedException();
        }
    }
    
    class Program
    {
        
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            var rules1 = RulesFactory.Create<IEmpresaRules>();
            var rules2 = RulesFactory.Create<IEmpresaFuncionarioRules>();

            var list2 = rules2.ListByFilter(EmpresaFuncionario.EmpresaProperty[Empresa.NomeProperty].Eq("OutraEmpresa"), OrderBy.None());
                        
        }
    }
}

