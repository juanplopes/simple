using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.ConfigSource;
using Simple.Services.Remoting;

namespace Simple.Tests
{
    [TestClass]
    public class Initializers
    {
        [AssemblyInitialize]
        public static void GlobalSetup(TestContext context)
        {
            DBEnsurer.Configure(typeof(DBEnsurer));
        }

        [AssemblyCleanup]
        public static void GlobalTeardown()
        {
            SourceManager.Do.Clear<RemotingConfig>();
        }
    }
}
