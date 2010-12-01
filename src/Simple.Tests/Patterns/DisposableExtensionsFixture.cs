using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Patterns;
using SharpTestsEx;

namespace Simple.Tests.Patterns
{
    [TestFixture]
    public class DisposableExtensionsFixture
    {
        [Test]
        public void WillNotThrowException()
        {
            int count = 0;
            var disposable1 = new DisposableAction(() => { throw new Exception(); });
            var newDisp = disposable1.ComposeWith(disposable1);
            newDisp.Dispose();

            count.Should().Be(0);
        }

        [Test]
        public void CanDisposeOneItem()
        {
            int count = 0;
            var disposable1 = new DisposableAction(() => count++);
            var newDisp = disposable1.ComposeWith();
            newDisp.Dispose();

            count.Should().Be(1);
        }

        [Test]
        public void CanDisposeTwoItemsLIFO()
        {
            int count = 7;
            var disposable1 = new DisposableAction(() => count %= 7);
            var disposable2 = new DisposableAction(() => count += 8);
            var newDisp = disposable1.ComposeWith(disposable2);
            newDisp.Dispose();

            count.Should().Be(1);
        }

        [Test]
        public void CanDisposeTwoItemsLIFO2()
        {
            int count = 7;
            var disposable1 = new DisposableAction(() => count %= 7);
            var disposable2 = new DisposableAction(() => count += 8);
            var newDisp = disposable2.ComposeWith(disposable1);
            newDisp.Dispose();

            count.Should().Be(8);
        }
    }
}
