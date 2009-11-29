using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Patterns;
using Simple.Reflection;
using System.Threading;

namespace Simple.Tests.SimpleLib
{
    [TestFixture]
    public class MiscFixture
    {
        [Test]
        public void TransformationListTest()
        {
            TransformationList<int> t = new TransformationList<int>();
            Func<int, int> func = x => x + 1;

            t.Add(func);
            t.Add(x => x + 2);

            Assert.AreEqual(4, t.Invoke(1));

            t.Remove(func);

            Assert.AreEqual(3, t.Invoke(1));
        }

        [Test]
        public void DisposableActionTest()
        {
            int x = 20;

            using (new DisposableAction(() => x++))
            {
                Assert.AreEqual(20, x);
            }

            Assert.AreEqual(21, x);
        }

        [Test]
        public void ThreadSingletonTest()
        {
            ThreadSingletonSample.Do.Test = 2;

            var t1 = new Thread(() =>
            {
                Assert.AreEqual(0, ThreadSingletonSample.Do.Test);
                ThreadSingletonSample.Do.Test = 3;
                Assert.AreEqual(3, ThreadSingletonSample.Do.Test);
            });

            t1.Start();
            t1.Join();

            Assert.AreEqual(2, ThreadSingletonSample.Do.Test);
        }

        [Test]
        public void SingletonTest()
        {
            SingletonSample.Do.Test = 2;

            var t1 = new Thread(() =>
            {
                Assert.AreEqual(2, SingletonSample.Do.Test);
                SingletonSample.Do.Test = 3;
                Assert.AreEqual(3, SingletonSample.Do.Test);
            });

            t1.Start();
            t1.Join();

            Assert.AreEqual(3, SingletonSample.Do.Test);
        }

        class ThreadSingletonSample : ThreadSingleton<ThreadSingletonSample>
        {
            public int Test { get; set; }
        }

        class SingletonSample : Singleton<SingletonSample>
        {
            public int Test { get; set; }
        }

    }


}
