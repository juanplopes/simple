using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    public class TestProvider : IWorkingDaysProvider
    {
        public bool IsWorkingDay(DateTime pobjDate)
        {
            return pobjDate.Day % 2 == 0;
        }

        public int GetNetWorkingDays(DateTime date1, DateTime date2)
        {
            int s = 0;
            for (; date1 <= date2; date1 = date1.AddDays(1))
            {
                if (IsWorkingDay(date1)) s++;
            }
            return s++;
        }
    }

    [TestFixture]
    public class WorkingDaysFixture : BaseWorkingDaysFixture
    {
        public WorkingDaysFixture() :
            base(new TestProvider(), 1000, 100, new DateTime(2008, 1, 1))
        { }

        #region ref1 ref2
        #region From back ref1 ref2
        [Test]
        public void Ref1Ref2FromToConsider()
        {
            var d = GeneralTest(true, true, true, true, true);
            new DateTime(2008, 01, 8).Should().Be(d);
        }

        [Test]
        public void Ref1Ref2FromToNot()
        {
            var d = GeneralTest(true, true, true, true, false);
            new DateTime(2008, 01, 6).Should().Be(d);
        }

        [Test]
        public void Ref1Ref2FromNotConsider()
        {
            var d = GeneralTest(true, true, true, false, true);
            new DateTime(2008, 01, 12).Should().Be(d);

        }

        [Test]
        public void Ref1Ref2FromNotNot()
        {
            var d = GeneralTest(true, true, true, false, false);
            new DateTime(2008, 01, 14).Should().Be(d);

        }
        #endregion

        #region To forward ref1 ref2
        [Test]
        public void Ref1Ref2NotToConsider()
        {
            var d = GeneralTest(true, true, false, true, true);
            new DateTime(2008, 01, 20).Should().Be(d);
        }

        [Test]
        public void Ref1Ref2NotToNot()
        {
            var d = GeneralTest(true, true, false, true, false);
            new DateTime(2008, 01, 18).Should().Be(d);
        }

        [Test]
        public void Ref1Ref2NotNotConsider()
        {
            var d = GeneralTest(true, true, false, false, true);
            new DateTime(2008, 01, 24).Should().Be(d);

        }

        [Test]
        public void Ref1Ref2NotNotNot()
        {
            var d = GeneralTest(true, true, false, false, false);
            new DateTime(2008, 01, 26).Should().Be(d);

        }
        #endregion
        #endregion

        #region ref1 not
        #region From back ref1 not
        [Test]
        public void Ref1NotFromToConsider()
        {
            var d = GeneralTest(true, false, true, true, true);
            new DateTime(2008, 01, 6).Should().Be(d);
        }

        [Test]
        public void Ref1NotFromToNot()
        {
            var d = GeneralTest(true, false, true, true, false);
            new DateTime(2008, 01, 6).Should().Be(d);
        }

        [Test]
        public void Ref1NotFromNotConsider()
        {
            var d = GeneralTest(true, false, true, false, true);
            new DateTime(2008, 01, 12).Should().Be(d);

        }

        [Test]
        public void Ref1NotFromNotNot()
        {
            var d = GeneralTest(true, false, true, false, false);
            new DateTime(2008, 01, 12).Should().Be(d);

        }
        #endregion

        #region To forward ref1 not
        [Test]
        public void Ref1NotNotToConsider()
        {
            var d = GeneralTest(true, false, false, true, true);
            new DateTime(2008, 01, 20).Should().Be(d);
        }

        [Test]
        public void Ref1NotNotToNot()
        {
            var d = GeneralTest(true, false, false, true, false);
            new DateTime(2008, 01, 20).Should().Be(d);
        }

        [Test]
        public void Ref1NotNotNotConsider()
        {
            var d = GeneralTest(true, false, false, false, true);
            new DateTime(2008, 01, 26).Should().Be(d);

        }

        [Test]
        public void Ref1NotNotNotNot()
        {
            var d = GeneralTest(true, false, false, false, false);
            new DateTime(2008, 01, 26).Should().Be(d);

        }
        #endregion
        #endregion


        #region not ref2
        #region From back not ref2
        [Test]
        public void NotRef2FromToConsider()
        {
            var d = GeneralTest(false, true, true, true, true);
            new DateTime(2008, 01, 8).Should().Be(d);
        }

        [Test]
        public void NotRef2FromToNot()
        {
            var d = GeneralTest(false, true, true, true, false);
            new DateTime(2008, 01, 6).Should().Be(d);
        }

        [Test]
        public void NotRef2FromNotConsider()
        {
            var d = GeneralTest(false, true, true, false, true);
            new DateTime(2008, 01, 12).Should().Be(d);

        }

        [Test]
        public void NotRef2FromNotNot()
        {
            var d = GeneralTest(false, true, true, false, false);
            new DateTime(2008, 01, 14).Should().Be(d);

        }
        #endregion

        #region To forward not ref2
        [Test]
        public void NotRef2NotToConsider()
        {
            var d = GeneralTest(false, true, false, true, true);
            new DateTime(2008, 01, 22).Should().Be(d);
        }

        [Test]
        public void NotRef2NotToNot()
        {
            var d = GeneralTest(false, true, false, true, false);
            new DateTime(2008, 01, 20).Should().Be(d);
        }

        [Test]
        public void NotRef2NotNotConsider()
        {
            var d = GeneralTest(false, true, false, false, true);
            new DateTime(2008, 01, 26).Should().Be(d);

        }

        [Test]
        public void NotRef2NotNotNot()
        {
            var d = GeneralTest(false, true, false, false, false);
            new DateTime(2008, 01, 28).Should().Be(d);

        }
        #endregion
        #endregion

        #region not not
        #region From back not not
        [Test]
        public void NotNotFromToConsider()
        {
            var d = GeneralTest(false, false, true, true, true);
            new DateTime(2008, 01, 8).Should().Be(d);
        }

        [Test]
        public void NotNotFromToNot()
        {
            var d = GeneralTest(false, false, true, true, false);
            new DateTime(2008, 01, 8).Should().Be(d);
        }

        [Test]
        public void NotNotFromNotConsider()
        {
            var d = GeneralTest(false, false, true, false, true);
            new DateTime(2008, 01, 14).Should().Be(d);

        }

        [Test]
        public void NotNotFromNotNot()
        {
            var d = GeneralTest(false, false, true, false, false);
            new DateTime(2008, 01, 14).Should().Be(d);

        }
        #endregion

        #region To forward not not
        [Test]
        public void NotNotNotToConsider()
        {
            var d = GeneralTest(false, false, false, true, true);
            new DateTime(2008, 01, 20).Should().Be(d);
        }

        [Test]
        public void NotNotNotToNot()
        {
            var d = GeneralTest(false, false, false, true, false);
            new DateTime(2008, 01, 20).Should().Be(d);
        }

        [Test]
        public void NotNotNotNotConsider()
        {
            var d = GeneralTest(false, false, false, false, true);
            new DateTime(2008, 01, 26).Should().Be(d);

        }

        [Test]
        public void NotNotNotNotNot()
        {
            var d = GeneralTest(false, false, false, false, false);
            new DateTime(2008, 01, 26).Should().Be(d);

        }
        #endregion
        #endregion


        protected DateTime GeneralTest(bool referenceIsBus, bool ref2IsBus, bool fromBack, bool toBack, bool considerCurrent)
        {
            DateTime ref1 = referenceIsBus ? new DateTime(2008, 01, 16) : new DateTime(2008, 01, 17);

            DateTime ref2 = fromBack ?
                (ref2IsBus == referenceIsBus ? ref1.AddDays(-6) : ref1.AddDays(-7)) :
                (ref2IsBus == referenceIsBus ? ref1.AddDays(6) : ref1.AddDays(7));
            if (toBack)
            {
                return WorkingDays.Get(new TestProvider()).GetBackwards(2, ref2, considerCurrent);
            }
            else
            {
                return WorkingDays.Get(new TestProvider()).GetInAdvance(2, ref2, considerCurrent);
            }

        }
    }
}
