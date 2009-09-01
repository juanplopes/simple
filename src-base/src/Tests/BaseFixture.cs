using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple;
using Conspirarte.Environment.Test;

namespace Conspirarte.Tests
{
    public class BaseFixture
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            Default.ConfigureServer().HostAssemblyOf(typeof(Server.Server));
        }
    }
}
