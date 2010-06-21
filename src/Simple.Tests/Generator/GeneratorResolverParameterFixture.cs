using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;
using System.Text.RegularExpressions;

namespace Simple.Tests.Generator
{
    public class GeneratorResolverParameterFixture
    {
        [Test]
        public void CanBindStringListWithOneParameter()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .ArgumentList(x => x.TestList);

            var generator = resolver.Resolve("sample test");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithoutParentesis()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .ArgumentList(x => x.TestList);

            var generator = resolver.Resolve("sample test, test2, test3");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test", "test2", "test3" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindIntListWithMultipleParameters()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleIntList(), "sample")
                .ArgumentList(x => x.TestList);

            var generator = resolver.Resolve("sample 1, 2, 3");

            Assert.IsInstanceOf<SampleIntList>(generator);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, (generator as SampleIntList).TestList);
        }

        [Test]
        public void CanBindIntNullableListWithMultipleParameters()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleIntNullableList(), "sample")
                .ArgumentList(x => x.TestList);

            var generator = resolver.Resolve("sample 1, 2, 3");

            Assert.IsInstanceOf<SampleIntNullableList>(generator);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, (generator as SampleIntNullableList).TestList);
        }

        [Test]
        public void CanBindInt()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .Argument(x => x.Test);

            var generator = resolver.Resolve("sample 1");

            Assert.IsInstanceOf<SampleSingleInt>(generator);
            Assert.AreEqual(1, (generator as SampleSingleInt).Test);
        }

        [Test]
        public void CannotBindIntWithZeroParameter()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .Argument(x => x.Test);

            Assert.Throws<ArgumentException>(() => resolver.Resolve("sample "), "invalid number of arguments: 0");
        }

        [Test]
        public void CannotBindIntWithTwoParameter()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .Argument(x => x.Test);

            Assert.Throws<ArgumentException>(() => resolver.Resolve("sample 1,2"), "invalid number of arguments: 2");
        }

        [Test]
        public void CanBindIntNullable()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleSingleIntNullable(), "sample")
                .Argument(x => x.Test);

            var generator = resolver.Resolve("sample 1");

            Assert.IsInstanceOf<SampleSingleIntNullable>(generator);
            Assert.AreEqual(1, (generator as SampleSingleIntNullable).Test);
        }


        [Test]
        public void CanBindIntNullableListWithNoParameters()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleIntNullableList(), "sample")
                .ArgumentList(x => x.TestList);

            var generator = resolver.Resolve("sample");

            Assert.IsInstanceOf<SampleIntNullableList>(generator);
            CollectionAssert.AreEqual(new int?[] { }, (generator as SampleIntNullableList).TestList);
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithParentesis()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .ArgumentList(x => x.TestList);

            var generator = resolver.Resolve("sample (test, test2, test3)");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test", "test2", "test3" }, (generator as SampleStringList).TestList);
        }

        public class SampleStringList : IGenerator
        {
            public IList<string> TestList { get; set; }

            public void Execute() { }
        }

        public class SampleIntList : IGenerator
        {
            public IList<int> TestList { get; set; }

            public void Execute() { }
        }

        public class SampleSingleInt : IGenerator
        {
            public int Test { get; set; }

            public void Execute() { }
        }

        public class SampleSingleIntNullable : IGenerator
        {
            public int? Test { get; set; }

            public void Execute() { }
        }


        public class SampleIntNullableList : IGenerator
        {
            public IList<int?> TestList { get; set; }

            public void Execute() { }
        }
    }
}
