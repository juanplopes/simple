using SimpleLibrary.Threading;
using BasicLibrary.Configuration;
using BasicLibrary.LibraryConfig;
using SimpleLibrary.Config;
using System;
using System.Diagnostics;
using System.Reflection;

namespace Sample.UserInterface2
{
    [DefaultFile("ConfigTest.config", ThrowException = false)]
    public class TestClass : ConfigElement
    {
        [ConfigElement("someStringValue")]
        public string SomeStringValue { get; set; }
        [ConfigElement("someIntValue")]
        public int SomeIntValue { get; set; }

        public override string DefaultXmlString
        {
            get { return "<a></a>"; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SimpleLibraryConfig.Get();
            long start = DateTime.Now.Ticks;
            for (int i = 0; i < 100; i++)
            {
                SimpleLibraryConfig config = SimpleLibraryConfig.Get();
            }
            TimeSpan end = new TimeSpan(DateTime.Now.Ticks - start);
        }
    }
}

