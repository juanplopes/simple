using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Common
{
    public abstract class BaseWorkingDaysProvider : IWorkingDaysProvider
    {
        #region IWorkingDaysProvider Members

        public abstract bool IsWorkingDay(DateTime date);

        public int GetNetWorkingDays(DateTime date1, DateTime date2)
        {
            if (date1 > date2)
                MathHelper.Swap(ref date1, ref date2);

            int s = 0;
            for (; date1 <= date2; date1 = date1.AddDays(1))
            {
                if (IsWorkingDay(date1)) s++;
            }
            return s;
        }

        #endregion
    }
}
