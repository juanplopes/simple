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

namespace Sample.UserInterface2
{
    [DefaultFile("TestConfig.config")]
    class TestConfig : ConfigRoot<TestConfig>
    {
        [ConfigElement("aSampleString")]
        public string SampleString { get; set; }

        [ConfigAcceptsParent("listOfStrings")]
        [ConfigElement("aString")]
        public List<string> ListOfStrings { get; set; }

        [ConfigAcceptsParent("dictionary")]
        [ConfigDictionaryKeyName("name")]
        [ConfigElement("valor")]
        public Dictionary<string, int> Dic { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(4000);
            IEmpresaRules rules = RulesFactory.Create<IEmpresaRules>();
            rules.TestMethod(null);
            IList<Empresa> list = rules.ListAll(null);

            MainLogger.Get<Program>().Debug("alguma mensagem");
            MainLogger.Get<Program>().Warn("warn message");   
        }
    }
}

