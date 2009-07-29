using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Simple.Logging;

[SetUpFixture]
public class SetupFixture
{
    [SetUp]
    public void Setup()
    {
        Log4netSimply.Do.Default();
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
    }

    void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Simply.Do.Log(this).Debug("ERROR:", e.ExceptionObject as Exception); ;
    }
}

