using System;
using System.Collections.Generic;
using System.Threading;
using Sample.BusinessInterface;
using Sample.BusinessInterface.Domain;
using SimpleLibrary.Filters;
using SimpleLibrary.Rules;
using BasicLibrary.Logging;
using BasicLibrary.Configuration;
using System.Globalization;
using SimpleLibrary.Config;
using SimpleLibrary.DataAccess;
using System.Web.UI;
using System.Collections;
using BasicLibrary.Common;

namespace Sample.UserInterface2
{
    public class TestBusinessDaysProvider : IBusinessDaysProvider
    {
        public bool IsBusinessDay(DateTime pobjDate)
        {
            return pobjDate.Day % 2 == 0;
        }
    }

    public class Test2 : IBusinessDaysProvider
    {

        #region IBusinessDaysProvider Members

        public bool IsBusinessDay(DateTime pobjDate)
        {
            return pobjDate.Day % 2 != 0;
        }

        #endregion
    }

    class Program
    {
        static IEnumerable Test()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return i;
            }
        }

        static void Main(string[] args)
        {
            var days = BusinessDays.Get(
                new CompositeBusinessDaysProvider(
                    new TestBusinessDaysProvider(),
                    new Test2()
                )
            );

            DateTime time = days.GetBackwards(1, DateTime.Now.AddDays(-1));
            DateTime asd = days.GetInAdvance(1, DateTime.Now.AddDays(-1));
        }
    }
}

