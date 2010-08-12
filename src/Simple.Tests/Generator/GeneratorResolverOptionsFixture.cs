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
    public class GeneratorResolverOptionsFixture
    {
        [Test]
        public void CanBindStringWithOptionAndSpaceWithoutParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleString(), "sample")
                .WithOption("with", x => x.Test);

            var generator = resolver.Resolve("sample with lasers");

            generator.Should().Be.OfType<SampleString>()
                 .And.Value.Test.Should().Be("lasers");
        }

        [Test]
        public void CanBindStringWithOptionAndEqualsWithoutParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleString(), "sample")
                .WithOption("with", x => x.Test);

            var generator = resolver.Resolve("sample with=lasers");

            generator.Should().Be.OfType<SampleString>()
                .And.Value.Test.Should().Be("lasers");
        }

        [Test]
        public void CanBindBooleanShorthand()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample +lasers");

            generator.Should().Be.OfType<SampleBoolean>()
                 .And.Value.Test.Should().Be.True();
        }

        [Test]
        public void CannotBindBooleanShorthandAndNormalAtSameTime()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            resolver.Executing(x => x.Resolve("sample +lasers:true"))
                .Throws<UnrecognizedOptionsException>();
        }

        [Test]
        public void CanBindBooleanNormal()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers=true");

            generator.Should().Be.OfType<SampleBoolean>()
                .And.Value.Test.Should().Be.True();
        }

        [Test]
        public void CanBindBooleanWithoutPassingItsValue()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample");

            generator.Should().Be.OfType<SampleBoolean>()
                .And.Value.Test.Should().Be.False();
        }


        [Test]
        public void CanBindBooleanNormalWithoutEquals()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers true");

            generator.Should().Be.OfType<SampleBoolean>()
                .And.Value.Test.Should().Be.True();
        }

        [Test]
        public void CanBindIntList()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntList(), "sample")
                .WithOptionList("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers:1,2,3");

            Assert.IsInstanceOf<SampleIntList>(generator);
            generator.Should().Be.OfType<SampleIntList>()
                .And.Value.Test.Should().Have.SameSequenceAs(1, 2, 3);
        }

        [Test]
        public void CanBindBoolList()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolList(), "sample")
                .WithOptionList("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers=t,f,y,n,yes,no,1,0,true,false");

            generator.Should().Be.OfType<SampleBoolList>()
                .And.Value.Test.Should().Have.SameSequenceAs(true, false, true, false, true, false, true, false, true, false);

        }

        [Test]
        public void CannotBindStringToMultipleParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleString(), "sample")
                .WithOption("with", x => x.Test);

            resolver.Executing(x => x.Resolve("sample with lasers, test "))
                .Throws<InvalidArgumentCountException>();
        }

        public class SampleString : ICommand
        {
            public string Test { get; set; }

            public void Execute() { }
        }

        public class SampleBoolean : ICommand
        {
            public bool Test { get; set; }

            public void Execute() { }
        }


        public class SampleIntList : ICommand
        {
            public IList<int?> Test { get; set; }

            public void Execute() { }
        }

        public class SampleBoolList : ICommand
        {
            public IList<bool> Test { get; set; }

            public void Execute() { }
        }

    }
}
