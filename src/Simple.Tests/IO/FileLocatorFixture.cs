using System.IO;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using Simple.IO;

namespace Simple.Tests.IO
{
    [TestFixture]
    public class FileLocatorFixture
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Directory.CreateDirectory("testdir");
            Directory.CreateDirectory("testdir/A");
            Directory.CreateDirectory("testdir/B");
            Directory.CreateDirectory("testdir/B/C");

            File.WriteAllText("testdir/A/1", string.Empty);
            File.WriteAllText("testdir/A/2", string.Empty);

            File.WriteAllText("testdir/B/2", string.Empty);
            File.WriteAllText("testdir/B/3", string.Empty);

            File.WriteAllText("testdir/B/C/3", string.Empty);
            File.WriteAllText("testdir/B/C/4", string.Empty);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Directory.Delete("testdir", true);
            Assert.IsFalse(Directory.Exists("testdir"));
        }

        [Test]
        public void TestSimpleSearches()
        {
            FileLocator locator = new FileLocator();
            locator.Add("testdir/A");
            locator.Add("testdir/B");
            locator.Add("testdir/B/C");

            var results = new[] { 1, 2, 3, 4 }.Select(x => locator.Find(x.ToString())).ToArray();

            StringAssert.StartsWith("testdir/A", results[0]);
            StringAssert.StartsWith("testdir/A", results[1]);
            StringAssert.StartsWith("testdir/B", results[2]);
            StringAssert.StartsWith("testdir/B/C", results[3]);
        }

        [Test, ExpectedException(typeof(FileNotFoundException), ExpectedMessage = "testdir/A, testdir/B, testdir/B/C", MatchType = MessageMatch.Contains)]
        public void TestNotFoundWithException()
        {
            FileLocator locator = new FileLocator();
            locator.Add("testdir/A");
            locator.Add("testdir/B");
            locator.Add("testdir/B/C");

            locator.Find("5");
        }

        [Test]
        public void TestNotFoundWithoutException()
        {
            FileLocator locator = new FileLocator();
            locator.Add("testdir/A");
            locator.Add("testdir/B");
            locator.Add("testdir/B/C");

            string file = locator.Find("5", false);
            Assert.IsNull(file);
        }
    }
}

