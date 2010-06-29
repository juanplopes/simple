using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;
using System.Text.RegularExpressions;

namespace Simple.Tests.Generator
{
    public class GeneratorResolverArgumentFixture
    {
        [Test]
        public void CanBindStringListWithOneParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample test");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithoutParentesis()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample test, test2, test3");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test", "test2", "test3" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindStringListWithSpecialChars()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample +test, @test2, t^est3");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "+test", "@test2", "t^est3" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindIntListWithMultipleParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample 1, 2, 3");

            Assert.IsInstanceOf<SampleIntList>(generator);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, (generator as SampleIntList).TestList);
        }

        [Test]
        public void CanBindIntNullableListWithMultipleParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntNullableList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample 1, 2, 3");

            Assert.IsInstanceOf<SampleIntNullableList>(generator);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, (generator as SampleIntNullableList).TestList);
        }

        [Test]
        public void CanBindInt()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .WithArgument(null, x => x.Test);

            var generator = resolver.Resolve("sample 1");

            Assert.IsInstanceOf<SampleSingleInt>(generator);
            Assert.AreEqual(1, (generator as SampleSingleInt).Test);
        }

        [Test]
        public void CanBindIntWithZeroParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .WithArgument(null, x => x.Test);

            var generator = resolver.Resolve("sample ");

            Assert.IsInstanceOf<SampleSingleInt>(generator);
            Assert.AreEqual(0, (generator as SampleSingleInt).Test);
        }

        [Test]
        public void CannotBindIntWithTwoParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .WithArgument(null, x => x.Test);

            Assert.Throws<InvalidArgumentCountException>(() => resolver.Resolve("sample 1,2"), "invalid number of arguments: 2");
        }

        [Test]
        public void CanBindIntNullable()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleIntNullable(), "sample")
                .WithArgument(null, x => x.Test);

            var generator = resolver.Resolve("sample 1");

            Assert.IsInstanceOf<SampleSingleIntNullable>(generator);
            Assert.AreEqual(1, (generator as SampleSingleIntNullable).Test);
        }


        [Test]
        public void CanBindIntNullableListWithNoParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntNullableList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample");

            Assert.IsInstanceOf<SampleIntNullableList>(generator);
            CollectionAssert.AreEqual(new int?[] { }, (generator as SampleIntNullableList).TestList);
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithParentesis()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample (test, test2, test3)");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test", "test2", "test3" }, (generator as SampleStringList).TestList);
        }

        [Test]
        public void CanBindNamedStringListQuoted()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList("asd", x => x.TestList);

            var generator = resolver.Resolve("sample 'test 1'");

            Assert.IsInstanceOf<SampleStringList>(generator);
            CollectionAssert.AreEqual(new[] { "test 1" }, (generator as SampleStringList).TestList);
        }


        public class SampleStringList : ICommand
        {
            public IList<string> TestList { get; set; }

            public void Execute() { }
        }

        public class SampleIntList : ICommand
        {
            public IList<int> TestList { get; set; }

            public void Execute() { }
        }

        public class SampleSingleInt : ICommand
        {
            public int Test { get; set; }

            public void Execute() { }
        }

        public class SampleSingleIntNullable : ICommand
        {
            public int? Test { get; set; }

            public void Execute() { }
        }


        public class SampleIntNullableList : ICommand
        {
            public IList<int?> TestList { get; set; }

            public void Execute() { }
        }
    }
}
