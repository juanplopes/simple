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

    
    
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            //for (int i = 0; i < 10000; i++)
            //{
            //    Empresa e = new Empresa()
            //    {
            //        Nome = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + i
            //    };
            //    rules.Persist(e);
            //}
            IList<Empresa> list = rules.ListAll(OrderBy.None());
        }

        static void Test<T>(Oi<T> q)
        {

        }
    }
}

