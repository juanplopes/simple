using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLibrary.Rules;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using System.Threading;
using SimpleLibrary.Filters;
using SimpleLibrary.DataAccess;
using System.Collections;
using SimpleLibrary.Config;
using System.Reflection;
using System.ServiceModel;
using BasicLibrary.Logging;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();

            Empresa e = new Empresa();
            e.Nome = "Living";
            rules.SaveOrUpdate(e);
            
            IList<Empresa> empresa = rules.ListByFilter(BooleanExpression.True, OrderBy.None());
        }
    }
}
