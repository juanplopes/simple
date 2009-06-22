using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Patterns;

namespace Simple.Tests.SimpleLib
{
    [TestClass]
    public class MiscFixture
    {
        [TestMethod]
        public void TransformationListTest()
        {
            TransformationList<int> t = new TransformationList<int>();
            Func<int,int> func = x => x + 1;
            
            t.Add(func);
            t.Add(x => x + 2);

            Assert.AreEqual(4, t.Invoke(1));

            t.Remove(func);

            Assert.AreEqual(3, t.Invoke(1));
        }
    }
}
