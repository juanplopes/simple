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

            Assert.IsInstanceOf<SampleString>(generator);
            (generator as SampleString).Test.Should().Be("lasers");
        }

        [Test]
        public void CanBindStringWithOptionAndEqualsWithoutParameter()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleString(), "sample")
                .WithOption("with", x => x.Test);

            var generator = resolver.Resolve("sample with=lasers");

            Assert.IsInstanceOf<SampleString>(generator);
            (generator as SampleString).Test.Should().Be("lasers");
        }

        [Test]
        public void CanBindBooleanShorthand()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample +lasers");

            Assert.IsInstanceOf<SampleBoolean>(generator);
            (generator as SampleBoolean).Test.Should().Be(true);
        }

        [Test]
        public void CannotBindBooleanShorthandAndNormalAtSameTime()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            Assert.Throws<UnrecognizedOptionsException>(
                ()=>resolver.Resolve("sample +lasers:true"));
        }

        [Test]
        public void CanBindBooleanNormal()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers=true");

            Assert.IsInstanceOf<SampleBoolean>(generator);
            (generator as SampleBoolean).Test.Should().Be(true);
        }

        [Test]
        public void CanBindBooleanWithoutPassingItsValue()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample");

            Assert.IsInstanceOf<SampleBoolean>(generator);
            (generator as SampleBoolean).Test.Should().Be(false);
        }


        [Test]
        public void CanBindBooleanNormalWithoutEquals()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .WithOption("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers true");

            Assert.IsInstanceOf<SampleBoolean>(generator);
            (generator as SampleBoolean).Test.Should().Be(true);
        }

        [Test]
        public void CanBindIntList()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleIntList(), "sample")
                .WithOptionList("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers:1,2,3");

            Assert.IsInstanceOf<SampleIntList>(generator);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, (generator as SampleIntList).Test);
        }

        [Test]
        public void CanBindBoolList()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleBoolList(), "sample")
                .WithOptionList("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers=t,f,y,n,yes,no,1,0,true,false");

            Assert.IsInstanceOf<SampleBoolList>(generator);
            CollectionAssert.AreEqual(new[] { true, false, true, false, true, false, true, false, true, false }, (generator as SampleBoolList).Test);
        }

        [Test]
        public void CannotBindStringToMultipleParameters()
        {
            var resolver = new CommandResolver();
            resolver.Register(() => new SampleString(), "sample")
                .WithOption("with", x => x.Test);

            Assert.Throws<InvalidArgumentCountException>(() => resolver.Resolve("sample with lasers, test "));
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
