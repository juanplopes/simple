using BasicLibrary.Configuration;
using NHibernate.Cfg;
using Sample.BusinessInterface.Domain;
using NHibernate.Mapping;
using SimpleLibrary.DataAccess;
using NHibernate;
using Sample.BusinessInterface;
using SimpleLibrary.Rules;
using System.Threading;
using SimpleLibrary.Config;
using SimpleLibrary.Filters;
using System.Collections.Generic;

namespace Sample.UserInterface2
{
    class Oi<T> { }
    class Oi2 : Oi<Oi2> { }

    class Program
    {
        static void Main(string[] args)
        {
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            IList<Empresa> list = rules.ListAll(OrderBy.None());
        }

        static void Test<T>(Oi<T> q)
        {
            
        }
    }
}

