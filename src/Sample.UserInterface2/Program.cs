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
using System.ServiceModel;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            List<string> s = new List<string>();
            s.Add("1144");
            s.Add("1145");

            rules.TestRules();
        }
    }
}

