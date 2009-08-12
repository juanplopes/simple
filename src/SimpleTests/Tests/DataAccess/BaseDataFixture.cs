using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Simple.Tests.DataAccess
{
    public class BaseDataFixture
    {
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            MySimply.Nop();
        }

        public Simply MySimply
        {
            get
            {
                return NHConfig1.Do.Simply;
            }
        }
    }
}
