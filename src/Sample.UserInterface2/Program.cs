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

namespace Sample.UserInterface2
{
    class Program
    {
        static void Main(string[] args)
        {
            ILog log = MainLogger.Get<Program>();
            while (true)
            {
                Thread.Sleep(100);
                log.Debug("oi");
            }
        }
    }
}

