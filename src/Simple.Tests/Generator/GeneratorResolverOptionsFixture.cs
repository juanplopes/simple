using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;
using System.Text.RegularExpressions;

namespace Simple.Tests.Generator
{
    public class GeneratorResolverOptionsFixture
    {
        [Test]
        public void CanBindStringWithOptionAndSpaceWithoutParameter()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleString(), "sample")
                .Option("with", x => x.Test);

            var generator = resolver.Resolve("sample with lasers");

            Assert.IsInstanceOf<SampleString>(generator);
            Assert.AreEqual("lasers", (generator as SampleString).Test);
        }

        [Test]
        public void CanBindStringWithOptionAndEqualsWithoutParameter()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleString(), "sample")
                .Option("with", x => x.Test);

            var generator = resolver.Resolve("sample with=lasers");

            Assert.IsInstanceOf<SampleString>(generator);
            Assert.AreEqual("lasers", (generator as SampleString).Test);
        }

        [Test]
        public void CanBindBooleanShorthand()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .Option("lasers", x => x.Test);

            var generator = resolver.Resolve("sample +lasers");

            Assert.IsInstanceOf<SampleBoolean>(generator);
            Assert.AreEqual(true, (generator as SampleBoolean).Test);
        }

        [Test]
        public void CanBindBooleanNormal()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .Option("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers=true");

            Assert.IsInstanceOf<SampleBoolean>(generator);
            Assert.AreEqual(true, (generator as SampleBoolean).Test);
        }

        [Test]
        public void CanBindBooleanNormalWithoutEquals()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleBoolean(), "sample")
                .Option("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers true");

            Assert.IsInstanceOf<SampleBoolean>(generator);
            Assert.AreEqual(true, (generator as SampleBoolean).Test);
        }

        [Test]
        public void CanBindIntList()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleIntList(), "sample")
                .OptionList("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers:1,2,3");

            Assert.IsInstanceOf<SampleIntList>(generator);
            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, (generator as SampleIntList).Test);
        }

        [Test]
        public void CanBindBoolList()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleBoolList(), "sample")
                .OptionList("lasers", x => x.Test);

            var generator = resolver.Resolve("sample lasers=t,f,y,n,yes,no,1,0,true,false");

            Assert.IsInstanceOf<SampleBoolList>(generator);
            CollectionAssert.AreEqual(new[] { true, false, true, false, true, false, true, false, true, false }, (generator as SampleBoolList).Test);
        }

        [Test]
        public void CannotBindStringToMultipleParameters()
        {
            var resolver = new GeneratorResolver();
            resolver.Register(() => new SampleString(), "sample")
                .Option("with", x => x.Test);

            Assert.Throws<ArgumentException>(() => resolver.Resolve("sample with lasers, test "));
        }

        public class SampleString : IGenerator
        {
            public string Test { get; set; }

            public void Execute() { }
        }

        public class SampleBoolean : IGenerator
        {
            public bool Test { get; set; }

            public void Execute() { }
        }


        public class SampleIntList : IGenerator
        {
            public IList<int?> Test { get; set; }

            public void Execute() { }
        }

        public class SampleBoolList : IGenerator
        {
            public IList<bool> Test { get; set; }

            public void Execute() { }
        }

    }
}
