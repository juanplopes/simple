using Sample.BusinessInterface;
using SimpleLibrary.Rules;
using SimpleLibrary.Filters;
using System.Collections.Generic;
using Sample.BusinessInterface.Domain;
using System.Threading;
using log4net;
using BasicLibrary.Logging;
using log4net.DateFormatter;
using log4net.Appender;
using System.Diagnostics;
using System;
using System.Globalization;
using SimpleLibrary.Config;
using System.Security.Cryptography;
using System.Security;
using BasicLibrary.Configuration;
using BasicLibrary.LibraryConfig;

namespace Sample.UserInterface2
{
    [DefaultFile("Test.config")]
    class Test : ConfigRoot<Test>
    {
        public class Test2 : ConfigElement, IStringConvertible
        {
            [ConfigElement("oi", Required=true)]
            public string Oi { get; set; }

            public void LoadFromString(string value)
            {
                Oi = value;
                (this as IConfigElement).NotifyLoad("oi");
            }
        }

        [ConfigElement("test2", Required=true)]
        public TypeConfigElement Test2Prop { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SimpleLibraryConfig config = SimpleLibraryConfig.Get();
        }
    }
}

