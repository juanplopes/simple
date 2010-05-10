using System;
using NUnit.Framework;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    [TestFixture]
    public class TransformationListFixture
    {
        [Test]
        public void TestNoTransformation()
        {
            TransformationList<int> t = new TransformationList<int>();
            Assert.AreEqual(1, t.Invoke(1));
        }

        [Test]
        public void TestAddOne()
        {
            TransformationList<int> t = new TransformationList<int>();
            t.Add(x => x + 2);
            Assert.AreEqual(3, t.Invoke(1));
        }

        [Test]
        public void TestAddTwo()
        {
            TransformationList<int> t = new TransformationList<int>();
            t.Add(x => x + 1);
            t.Add(x => x + 2);
            Assert.AreEqual(4, t.Invoke(1));
        }



        [Test]
        public void TestRecursion()
        {
            TransformationList<int> t = new TransformationList<int>();
            Func<int, int> func = null;
            func = x => (x < 100 ? func(x + 1) : x);

            t.Add(func);

            Assert.AreEqual(100, t.Invoke(1));
        }


        [Test]
        public void TestAddTwoThenRemoveOne()
        {
            TransformationList<int> t = new TransformationList<int>();
            Func<int, int> func = x => x + 1;

            t.Add(func);
            t.Add(x => x + 2);

            Assert.AreEqual(4, t.Invoke(1));

            t.Remove(func);

            Assert.AreEqual(3, t.Invoke(1));
        }
    }
}
