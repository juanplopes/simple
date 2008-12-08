using System;
using System.Collections.Generic;
using System.Threading;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;
using BasicLibrary.Logging;
using BasicLibrary.Configuration;
using System.Globalization;
using SimpleLibrary.Config;
using SimpleLibrary.DataAccess;
using System.Web.UI;
using System.Collections;
using BasicLibrary.Common;
using System.Text.RegularExpressions;
using System.Reflection;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            IList<Empresa> list = rules.ListAll(OrderBy.None());
        }
    }
}

