using System.Collections.Generic;
using SimpleLibrary.Rules;
using Sample.BusinessInterface.Rules;
using SimpleLibrary.Filters;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface.Domain;
using System.IO;
using System.Text.RegularExpressions;
using System;
using BasicLibrary.Logging;

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RulesFactory.Create<IEmpresaRules>().HeartBeat();
            }
            catch (Exception e)
            {
                MainLogger.Get<Program>().Error(e.Message, e);
            }
            Console.ReadLine();
        }
    }
}

