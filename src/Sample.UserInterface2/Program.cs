using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text;
using System.Threading;
using BasicLibrary.Logging;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Config;
using SimpleLibrary.DataAccess;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;
using SimpleLibrary.ServiceModel;
using System.Globalization;
using System.Reflection.Emit;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
      

            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            Thread.Sleep(4000);
            Empresa e = rules.Load(1);
        }
    }
}
