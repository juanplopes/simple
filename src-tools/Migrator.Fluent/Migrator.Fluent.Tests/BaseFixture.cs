using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using NUnit.Framework;

namespace Migrator.Fluent.Tests
{
    public class BaseFixture
    {
        public MockRepository Mocks { get; set; }

        [TestFixtureSetUp]
        public virtual void FixtureSetup()
        {
            Mocks = new MockRepository();
        }

        [TestFixtureTearDown]
        public virtual void TearDown()
        {
            Mocks.VerifyAll();
        }
    }
}
