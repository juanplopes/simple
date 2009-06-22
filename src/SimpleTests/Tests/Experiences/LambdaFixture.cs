using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using Simple.Persistence;
using Simple.IO;

namespace Simple.Tests.Experiences
{
    [TestClass]
    public class LambdaFixture
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void TestSerializeExpression()
        {
            Expression<Predicate<int>> lambda = 
                x => x == 2 && x == 3;

            XmlHelper.QuickSerialize(lambda);
        }
    }
}
