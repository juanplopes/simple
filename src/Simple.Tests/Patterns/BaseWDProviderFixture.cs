using System;
using System.Collections.Generic;
using NUnit.Framework;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    public class TestBaseProvider : BaseWorkingDaysProvider
    {

        public override IEnumerable<DayOfWeek> FixedNonWorkingDays()
        {
            yield return DayOfWeek.Thursday;
            yield return DayOfWeek.Monday;
            yield return DayOfWeek.Friday;
            yield return DayOfWeek.Friday;
        }

        public override int GetNetDynamicNonWorkingDays(DateTime date1, DateTime date2)
        {
            return (new DateTime(2008, 1, 19) >= date1 && new DateTime(2008, 1, 19) <= date2) ? 1 : 0;
        }

        public override bool IsDynamicNonWorkingDay(DateTime date)
        {
            return date == new DateTime(2008, 1, 19);
        }
    }

    [TestFixture]
    public class BaseWDProviderTests : BaseWorkingDaysFixture
    {
        public BaseWDProviderTests()
            : base(
                new TestBaseProvider(), 1000, 100, new DateTime(2008, 1, 17))
        {

        }
    }
}
