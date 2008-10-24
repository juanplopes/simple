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
using SimpleLibrary.ServiceModel;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();

            IList<Empresa> list = rules.ListByFilter(Empresa.NomeProperty.Like("living", false), OrderBy.None());

            SimpleContext.Get().CustomData["teste"] = "oi";

            rules.GetOne();
            
        }
    }
}
