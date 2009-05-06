using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Simple.NUnit
{
    public delegate void TestDelegate();

    public class EntityTestsOrchestrator
    {
        protected IList<BaseTests> Tests = new List<BaseTests>();
        
        protected void AddType(Type type, bool defaultSkipId)
        {
            this.Tests.Add(new BaseTests(type, defaultSkipId));
        }

        protected void AddType(Type type)
        {
            AddType(type, true);
        }

        protected void ClearTypes()
        {
            this.Tests.Clear();
        }

        [Test]
        public void InsertScript1()
        {
            foreach (BaseTests test in Tests)
            {
                test.InsertScript1();
            }
        }

        [Test]
        public void UpdateScript1()
        {
            foreach (BaseTests test in Tests)
            {
                test.UpdateScript1();
            }
        }
    }
}
