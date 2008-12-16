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
using System.Xml;
using System.IO;
using System.Text;
using Sample.BusinessInterface.Rules;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmpresaFuncionarioRules1 rules = RulesFactory.Create<IEmpresaFuncionarioRules1>();
            rules.Save(new EmpresaFuncionario());
        }
    }
}

