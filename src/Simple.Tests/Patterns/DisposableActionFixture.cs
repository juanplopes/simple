using NUnit.Framework;
using SharpTestsEx;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    [TestFixture]
    public class DisposableActionFixture
    {
        [Test]
        public void TestNormalPath()
        {
            int x = 20;

            using (new DisposableAction(() => x++))
            {
                x.Should().Be(20);
            }

            x.Should().Be(21);
        }
        [Test]
        public void TestNormalPathEnsuring()
        {
            int x = 20;

            using (new DisposableAction(() => x++, true))
            {
                x.Should().Be(20);
            }

            x.Should().Be(21);
        }



        [Test]
        public void TestDisposeAsManyTimesAsNeeded()
        {
            int x = 20;

            using (var d = new DisposableAction(() => x++))
            {
                using (d)
                    x.Should().Be(20);
            }

            x.Should().Be(22);
        }

        [Test]
        public void TestDisposeOnlyOneTime()
        {
            int x = 20;

            using (var d = new DisposableAction(() => x++, true))
            {
                using(d)
                    x.Should().Be(20);
            }

            x.Should().Be(21);
        }

       

    }


}
