using System.Threading;
using NUnit.Framework;
using SharpTestsEx;
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
                ThreadSingletonSample.Do.Test.Should().Be(0);
                ThreadSingletonSample.Do.Test = 3;
                ThreadSingletonSample.Do.Test.Should().Be(3);
            });

            t1.Start();
            t1.Join();

            ThreadSingletonSample.Do.Test.Should().Be(2);
        }

        [Test]
        public void SingletonTest()
        {
            SingletonSample.Do.Test = 2;

            var t1 = new Thread(() =>
            {
                SingletonSample.Do.Test.Should().Be(2);
                SingletonSample.Do.Test = 3;
                SingletonSample.Do.Test.Should().Be(3);
            });

            t1.Start();
            t1.Join();

            SingletonSample.Do.Test.Should().Be(3);
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
