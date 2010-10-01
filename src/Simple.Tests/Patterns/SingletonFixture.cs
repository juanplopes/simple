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
            SimpleContext.Data.Singleton<ThreadSingletonSample>().Test = 2;
           

            var t1 = new Thread(() =>
            {
                SimpleContext.Data.Singleton<ThreadSingletonSample>().Test.Should().Be(0);
                SimpleContext.Data.Singleton<ThreadSingletonSample>().Test = 3;
                SimpleContext.Data.Singleton<ThreadSingletonSample>().Test.Should().Be(3);
            });

            t1.Start();
            t1.Join();

            SimpleContext.Data.Singleton<ThreadSingletonSample>().Test.Should().Be(2);
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

        class ThreadSingletonSample
        {
            public int Test { get; set; }
        }

        class SingletonSample : Singleton<SingletonSample>
        {
            public int Test { get; set; }
        }
    }
}
