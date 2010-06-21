using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;
using System.Text.RegularExpressions;

namespace Simple.Tests.Generator
{
    [Explicit]
    public class GeneratorResolverParameterFixture
    {
        [Test]
        public void CanBindStringListWithOneParameter()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .Argument(x => x.TestList);

            var generator = resolver.Resolve("sample test");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithoutParentesis()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .Argument(x => x.TestList);

            var generator = resolver.Resolve("sample test, test2, test3");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test", "test2", "test3" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithParentesis()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .Argument(x => x.TestList);

            var generator = resolver.Resolve("sample (test, test2, test3)");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test", "test2", "test3" }, (generator as SampleStringList).TestList);
        }

        public class SampleStringList : IGenerator
        {
            public IList<string> TestList { get; set; }

            public void Execute() { }
        }

        public class SampleDateTimeList : IGenerator
        {
            public IList<DateTime> TestList { get; set; }

            public void Execute() { }
        }
    }
}
