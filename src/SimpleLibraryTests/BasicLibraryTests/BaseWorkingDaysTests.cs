using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using BasicLibrary.Common;

namespace SimpleLibrary.BasicLibraryTests
{
    public class BaseWorkingDaysTests
    {
        protected static void AssertProviderSoft(IWorkingDaysProvider prov, int iterations, DateTime baseDate)
        {
            int count = 0;
            DateTime curr = baseDate;
            for (int i = 0; i < iterations; i++)
            {
                if (prov.IsWorkingDay(curr))
                    count++;

                Assert.AreEqual(count, prov.GetNetWorkingDays(baseDate, curr));

                curr = curr.AddDays(1);
            }
        }

        protected static void AssertProviderHeavy(IWorkingDaysProvider prov, int iterations, DateTime baseDate, bool forward, bool consider)
        {
            WorkingDays w = new WorkingDays(prov);

            int one = forward ? 1 : -1;

            DateTime curr = baseDate;
            for (int i = 1; i <= iterations; i++)
            {
                while (!w.IsWorkingDay(curr) || (!consider && curr == baseDate))
                    curr = curr.AddDays(one);

                if (forward)
                    Assert.AreEqual(curr, w.GetInAdvance(i, baseDate, consider));
                else
                    Assert.AreEqual(curr, w.GetBackwards(i, baseDate, consider));

                curr = curr.AddDays(one);
            }
        }

        protected IWorkingDaysProvider Provider { get; private set; }
        protected int SoftIterations { get; private set; }
        protected int HeavyIterations { get; private set; }
        protected DateTime BaseDate { get; private set; }

        protected BaseWorkingDaysTests(IWorkingDaysProvider provider, int softIterations, int heavyIterations, DateTime baseDate)
        {
            Provider = provider;
            SoftIterations = softIterations;
            HeavyIterations = heavyIterations;
            BaseDate = baseDate;
        }

        [Test]
        public void SoftTest()
        {
            AssertProviderSoft(Provider, SoftIterations, BaseDate);
        }

        [Test]
        public void HeavyForwardConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, BaseDate, true, true);
        }

        [Test]
        public void HeavyForwardNotConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, BaseDate, true, false);
        }

        [Test]
        public void HeavyBackwardConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, BaseDate, false, true);
        }

        [Test]
        public void HeavyBackwardNotConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, BaseDate, false, false);
        }

    }
}
