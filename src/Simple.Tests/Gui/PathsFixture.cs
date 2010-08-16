using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Gui;

namespace Simple.Tests.Gui
{
    [TestFixture]
    public class PathsFixture
    {
        [Test]
        public void CanFindIndividualPathItems()
        {
            var pathStr = @"c:\whatever;d:\omg";

            var path = new Paths(pathStr);

            path.Should().Have.SameSequenceAs(
                @"c:\whatever", @"d:\omg");
        }

        [Test]
        public void CanGetPathStringFromPathList()
        {
            var path = new Paths("");
            path.Add(@"c:\a");
            path.Add(@"d:\b");
            path.Add(@"e:/C");

            path.ToString().Should().Be(@"c:\a\;d:\b\;e:\c\");
        }

        [Test]
        public void CanCheckForPathExistenceUsingSameTrailingSlashNormalization()
        {
            var pathStr = @"c:\whatever;d:\omg";

            var path = new Paths(pathStr);

            path.Contains(@"c:\whatever").Should().Be.True();
        }


        [Test]
        public void CanCheckForPathExistenceUsingDifferentTrailingSlashNormalization()
        {
            var pathStr = @"c:\whatever;d:\omg";

            var path = new Paths(pathStr);

            path.Contains(@"c:\whatever\").Should().Be.True();
        }
        
        [Test]
        public void CanCheckForPathExistenceUsingNormalSlashInsteadOfBackslash()
        {
            var pathStr = @"c:\whatever;d:\omg";

            var path = new Paths(pathStr);

            path.Contains(@"c:/whatever/").Should().Be.True();
        }

        [Test]
        public void CanCheckForPathExistenceUsingDifferentCasing()
        {
            var pathStr = @"c:\whatever;d:\omg";

            var path = new Paths(pathStr);

            path.Contains(@"C:/Whatever/").Should().Be.True();
        }

        [Test]
        public void FindingPathWithSamePrefixButDifferentLocationWillNotReturnTrue()
        {
            var pathStr = @"c:\whatever;d:\omg";

            var path = new Paths(pathStr);

            path.Contains(@"C:/Whatever.blablabla").Should().Be.False();
        }

        [Test]
        public void AddingNonNormalizedPathWillNormalizeAndAddItToList()
        {
            var pathStr = @"c:\whatever;d:\omg";

            var path = new Paths(pathStr);
            
            path.Add("E:/Wtf").Should().Be(@"e:\wtf\");

            path.Should().Have.SameSequenceAs(
                @"c:\whatever", @"d:\omg", @"e:\wtf\");
        }
       
    }
}
