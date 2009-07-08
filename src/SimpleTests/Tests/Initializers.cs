using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.ConfigSource;
using Simple.Remoting;

namespace Simple.Tests
{
    [TestClass]
    public class Initializers
    {
        [AssemblyCleanup]
        public static void GlobalTeardown()
        {
            SourceManager.Do.Clear<RemotingConfig>();
        }
    }
}
