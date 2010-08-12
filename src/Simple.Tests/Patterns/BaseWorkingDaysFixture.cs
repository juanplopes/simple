using System;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    public abstract class BaseWorkingDaysFixture
    {
        protected static void AssertProviderSoft(IWorkingDaysProvider prov, int iterations, params DateTime[] baseDates)
        {
            foreach (var baseDate in baseDates)
            {
                int count = 0;
                DateTime curr = baseDate;
                for (int i = 0; i < iterations; i++)
                {
                    if (prov.IsWorkingDay(curr))
                        count++;

                    prov.GetNetWorkingDays(baseDate, curr).Should().Be(count);

                    curr = curr.AddDays(1);
                }
            }
        }

        protected static void AssertProviderHeavy(IWorkingDaysProvider prov, int iterations, bool forward, bool consider, params DateTime[] baseDates)
        {
            foreach (var baseDate in baseDates)
            {
                WorkingDays w = new WorkingDays(prov);

                int one = forward ? 1 : -1;

                DateTime curr = baseDate;
                for (int i = 1; i <= iterations; i++)
                {
                    while (!w.IsWorkingDay(curr) || (!consider && curr == baseDate))
                        curr = curr.AddDays(one);

                    if (forward)
                        w.GetInAdvance(i, baseDate, consider).Should().Be(curr);
                    else
                        w.GetBackwards(i, baseDate, consider).Should().Be(curr);

                    curr = curr.AddDays(one);
                }
            }
        }

        protected IWorkingDaysProvider Provider { get; private set; }
        protected int SoftIterations { get; private set; }
        protected int HeavyIterations { get; private set; }
        protected DateTime[] BaseDates { get; private set; }

        protected BaseWorkingDaysFixture(IWorkingDaysProvider provider, int softIterations, int heavyIterations, params DateTime[] baseDates)
        {
            Provider = provider;
            SoftIterations = softIterations;
            HeavyIterations = heavyIterations;
            BaseDates = baseDates;
        }

        [Test]
        public void SoftTest()
        {
            AssertProviderSoft(Provider, SoftIterations, BaseDates);
        }

        [Test]
        public void HeavyForwardConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, true, true, BaseDates);
        }

        [Test]
        public void HeavyForwardNotConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, true, false, BaseDates);
        }

        [Test]
        public void HeavyBackwardConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, false, true, BaseDates);
        }

        [Test]
        public void HeavyBackwardNotConsiderTest()
        {
            AssertProviderHeavy(Provider, HeavyIterations, false, false, BaseDates);
        }

    }
}
