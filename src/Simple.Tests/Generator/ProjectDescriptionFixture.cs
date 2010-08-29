using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;
using SharpTestsEx;

namespace Simple.Tests.Generator
{
    [TestFixture]
    public class ProjectDescriptionFixture
    {
        [Test]
        public void CanCreateBaseDescription()
        {
            var desc = new ProjectDescription("a", "b", "c");
            desc.Directory.Should().Be("a");
            desc.ProjectFile.Should().Be("b");
            desc.Assembly.Should().Be("c");
        }

        [Test]
        public void CanCreateDerivedDescription()
        {
            var desc = new ProjectDescription("a{0}", "b{0}", "c{0}");
            var desc2 = desc.WithName("123");

            desc2.Should().Not.Be.SameInstanceAs(desc);
            desc2.Directory.Should().Be("a123");
            desc2.ProjectFile.Should().Be("b123");
            desc2.Assembly.Should().Be("c123");
        }
    }
}
