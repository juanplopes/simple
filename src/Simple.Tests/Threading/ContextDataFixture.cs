using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Threading;
using SharpTestsEx;
using System.Threading;

namespace Simple.Tests.Threading
{
    [TestFixture]
    public class ContextDataFixture
    {
        [Test]
        public void DifferentContextDataWithSameProviderWillReturnSameValues()
        {
            var provider = new DictionaryContextProvider();
            var data1 = new ContextData(provider);
            var data2 = new ContextData(provider);

            data1.Set("abc", 123);

            data1.Get<int>("abc").Should().Be(123);
            data2.Get<int>("abc").Should().Be(123);
        }

        [Test]
        public void DifferentContextDataWithDifferentProvidersWillReturnDifferentValues()
        {
            var data1 = new ContextData(new DictionaryContextProvider());
            var data2 = new ContextData(new DictionaryContextProvider());

            data1.Set("abc", 123);

            data1.Get<int>("abc").Should().Be(123);
            data2.Get<int>("abc").Should().Be(0);
            data2.Get("abc").Should().Be(null);
        }

        [Test]
        public void CanEnsureSingletonForTypeWithDifferentConstructor()
        {
            var data = new ContextData(new DictionaryContextProvider());

            var sample1 = data.Singleton<Sample>(() => new Sample(1));
            var sample2 = data.Singleton<Sample>(() => new Sample(1));

            sample1.Should().Be.SameInstanceAs(sample2);
        }

        [Test]
        public void CanSetSingletonForTypeWithDifferentConstructor()
        {
            var data = new ContextData(new DictionaryContextProvider());

            var sample1 = data.Singleton<Sample>(() => new Sample(1));

            var sample3 = new Sample(2);
            data.SetSingleton<Sample>(sample3);
            var sample2 = data.Singleton<Sample>(() => new Sample(1));

            sample1.Should().Not.Be.SameInstanceAs(sample2);
            sample2.Should().Be.SameInstanceAs(sample3);
        }

        [Test]
        public void CanEnsureSingletonUsingThreadProvider()
        {
            var data = new ContextData(new ThreadDataProvider());

            var sample1 = data.Singleton<SampleDefault>();

            var thread = new Thread(() =>
            {
                var inside1 = data.Singleton<SampleDefault>();
                var inside2 = data.Singleton<SampleDefault>();

                inside1.Should().Not.Be.SameInstanceAs(sample1);
                inside2.Should().Be.SameInstanceAs(inside2);
            });

            thread.Start();
            thread.Join();

            var sample2 = data.Singleton<SampleDefault>();

            sample1.Should().Be.SameInstanceAs(sample2);
        }

        [Test]
        public void CanFeedGenericProviderWithDictionaryConstructor()
        {
            var dic = new Dictionary<object, object>();
            var data = new ContextData(new GenericContextProvider(() => dic));

            data.Set("test", 123);
            data.Get("test").Should().Be(123);

            data.Set("test2", 124);
            data.Get("test2").Should().Be(124);

            dic.Count.Should().Be(1);

            dic = new Dictionary<object, object>();
            data.Get("test").Should().Be(null);
        }

        [Test]
        public void CanFeedGenericProviderWithTwoDictionariesConstructor()
        {
            var dic = new Dictionary<object, object>();
            var data = new ContextData(new GenericContextProvider(() => dic));
            var data2 = new ContextData(new GenericContextProvider(() => dic));

            data.Set("test", 123);
            data.Get("test").Should().Be(123);

            data2.Set("test", 124);
            data2.Get("test").Should().Be(124);

            dic.Count.Should().Be(2);

            dic = new Dictionary<object, object>();
            dic.Count.Should().Be(0);
            data.Get("test").Should().Be(null);
            data2.Get("test").Should().Be(null);
        }


        class Sample
        {
            public Sample(int i)
            {
            }
        }

        class SampleDefault
        {
        }

    }
}
