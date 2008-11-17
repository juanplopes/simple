using Sample.BusinessInterface;
using SimpleLibrary.Rules;
using SimpleLibrary.Filters;
using System.Collections.Generic;
using Sample.BusinessInterface.Domain;
using System.Threading;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            Empresa e = new Empresa();
            e.Nome = "okok";
            e = rules.Save(e);
        }
    }
}

