using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Simple.Logging;
using System.IO;

[SetUpFixture]
class SetupFixture
{
    [SetUp]
    public void Setup()
    {
        Simply.Do.Configure.Log4netToConsole();
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

        File.WriteAllBytes("Northwind.sl3", Database.Northwind);
    }

    void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Simply.Do.Log(this).Debug("ERROR:", e.ExceptionObject as Exception); ;
    }
}

