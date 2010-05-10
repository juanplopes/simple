using System.Threading;
using NUnit.Framework;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    [TestFixture]
    public class SingletonFixture
    {
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
