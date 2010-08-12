using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
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

            generator.Should().Be.OfType<SampleStringList>()
                .And.Value.TestList.Should().Have.SameSequenceAs(new[] { "test" });
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithoutParentesis()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample test, test2, test3");

            generator.Should().Be.OfType<SampleStringList>()
                .And.Value.TestList.Should().Have.SameSequenceAs(new[] { "test", "test2", "test3" });
        }

        [Test]
        public void CanBindStringListWithSpecialChars()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample +test, @test2, t^est3");

            generator.Should().Be.OfType<SampleStringList>()
                .And.Value.TestList.Should().Have.SameSequenceAs(new[] { "+test", "@test2", "t^est3" });
        }

        [Test]
        public void CanBindIntListWithMultipleParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample 1, 2, 3");

            generator.Should().Be.OfType<SampleIntList>()
                .And.Value.TestList.Should().Have.SameSequenceAs(new[] { 1, 2, 3 });
        }

        [Test]
        public void CanBindIntNullableListWithMultipleParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntNullableList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample 1, 2, 3");

            generator.Should().Be.OfType<SampleIntNullableList>()
                .And.Value.TestList.Should().Have.SameSequenceAs(new int?[] { 1, 2, 3 });
        }

        [Test]
        public void CanBindInt()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .WithArgument(null, x => x.Test);

            var generator = resolver.Resolve("sample 1");

            generator.Should().Be.OfType<SampleSingleInt>()
                 .And.Value.Test.Should().Be(1);
        }

        [Test]
        public void CanBindIntWithZeroParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .WithArgument(null, x => x.Test);

            var generator = resolver.Resolve("sample ");

            generator.Should().Be.OfType<SampleSingleInt>()
                 .And.Value.Test.Should().Be(0);
        }

        [Test]
        public void CannotBindIntWithTwoParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleInt(), "sample")
                .WithArgument(null, x => x.Test);

            resolver.Executing(x=> x.Resolve("sample 1,2"))
                .Throws<InvalidArgumentCountException>()
                .And.Exception.Message.Should().Be("Invalid argument count in ' 1,2'. Expected: 1. Found: 2");
        }

        [Test]
        public void CanBindIntNullable()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleSingleIntNullable(), "sample")
                .WithArgument(null, x => x.Test);

            var generator = resolver.Resolve("sample 1");

            generator.Should().Be.OfType<SampleSingleIntNullable>()
                .And.Value.Test.Should().Be(1);
        }


        [Test]
        public void CanBindIntNullableListWithNoParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntNullableList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample");

            generator.Should().Be.OfType<SampleIntNullableList>()
                .And.Value.TestList.Should().Be.Empty();
        }

        [Test]
        public void CanBindStringListWithMultipleParametersWithParentesis()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList(null, x => x.TestList);

            var generator = resolver.Resolve("sample (test, test2, test3)");

            generator.Should().Be.OfType<SampleStringList>()
                .And.Value.TestList.Should().Have.SameSequenceAs("test", "test2", "test3");
        }

        [Test]
        public void CanBindNamedStringListQuoted()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleStringList(), "sample")
                .WithArgumentList("asd", x => x.TestList);

            var generator = resolver.Resolve("sample 'test 1'");

            generator.Should().Be.OfType<SampleStringList>()
                .And.Value.TestList.Should().Have.SameSequenceAs("test 1");
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
