using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Generator.Misc;

namespace Simple.Tests.Generator
{
    [TestFixture]
    public class InterfaceReplacerFixture
    {
        [Test]
        public void CanOverrideOnlyOneInterface()
        {
            var data = @"tes test test
                public class TestAbc : IWhatever
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceHide(data, "IWhatever", "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IService<IWhatever>
                {
                }");

        }

        [Test]
        public void CanUnOverrideOnlyOneInterface()
        {
            var data = @"tes test test
                public class TestAbc : IService<IWhatever>
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceShow(data, "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IWhatever
                {
                }");

        }

        [Test]
        public void CanOverrideThreeInterfaceWhereItsTheSecond()
        {
            var data = @"tes test test
                public class TestAbc : IOther, IWhatever, IMustRemain
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceHide(data, "IWhatever", "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IOther, IService<IWhatever>, IMustRemain
                {
                }");

        }

        [Test]
        public void WillNotOverrideIfAlreadyOverriden()
        {
            var data = @"tes test test
                public class TestAbc : IOther, IService<IWhatever>, IMustRemain
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceHide(data, "IWhatever", "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IOther, IService<IWhatever>, IMustRemain
                {
                }");

        }

        [Test]
        public void CanUnOverrideThreeInterfaceWhereItsTheSecond()
        {
            var data = @"tes test test
                public class TestAbc : IOther, IService<IWhatever>, IService<IMustRemain>
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceShow(data, "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IOther, IWhatever, IMustRemain
                {
                }");

        }


        [Test]
        public void CanOverrideTwoInterfaceWhereItsTheSecond()
        {
            var data = @"tes test test
                public class TestAbc : IOther, IWhatever
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceHide(data, "IWhatever", "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IOther, IService<IWhatever>
                {
                }");

        }

        [Test]
        public void CanOverrideTwoInterfaceWhereItsTheSecondWithFullNamespace()
        {
            var data = @"tes test test
                public class TestAbc : IOther, Full.Namespace.IWhatever
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceHide(data, "IWhatever", "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IOther, IService<Full.Namespace.IWhatever>
                {
                }");

        }

        [Test]
        public void CanOverrideTwoInterfaceWhereItsTheFirst()
        {
            var data = @"tes test test
                public class TestAbc : IOther, IWhatever
                {
                }";

            var replaced = new CSharpInterfaceReplacer().ReplaceHide(data, "IOther", "IService");

            replaced.Should().Be(@"tes test test
                public class TestAbc : IService<IOther>, IWhatever
                {
                }");

        }

    }
}
