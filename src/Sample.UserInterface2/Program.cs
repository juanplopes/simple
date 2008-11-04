using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Config;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;
using BasicLibrary.Configuration;
using BasicLibrary.Logging;
using System.IO;
using System.Diagnostics;

namespace Sample.UserInterface2
{
   class Program
    {
        static void Main(string[] args)
        {
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            IList<Empresa> list = rules.ListByFilter(
                Empresa.NomeProperty.Contains("a"), OrderBy.Asc(Empresa.IdEmpresaProperty).Desc(Empresa.NomeProperty));
        }
    }
}

