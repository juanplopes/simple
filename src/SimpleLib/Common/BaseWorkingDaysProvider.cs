using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Common
{
    public abstract class BaseWorkingDaysProvider : IWorkingDaysProvider
    {
        public abstract IEnumerable<DayOfWeek> FixedNonWorkingDays();
        public abstract int GetNetDynamicNonWorkingDays(DateTime date1, DateTime date2);
        public abstract bool IsDynamicNonWorkingDay(DateTime date);

        private HashSet<DayOfWeek> _fixedNonSet = null;
        protected HashSet<DayOfWeek> FixedNonSet
        {
            get
            {
                lock (this)
                {
                    if (_fixedNonSet == null)
                    {
                        _fixedNonSet = new HashSet<DayOfWeek>(FixedNonWorkingDays());
                    }
                    return _fixedNonSet;
                }
            }
        }





        #region IWorkingDaysProvider Members

        public bool IsWorkingDay(DateTime date)
        {
            if (FixedNonSet.Contains(date.DayOfWeek)) return false;
            if (IsDynamicNonWorkingDay(date)) return false;
            return true;
        }

        public int GetNetWorkingDays(DateTime date1, DateTime date2)
        {
            date1 = date1.Date;
            date2 = date2.Date;

            int count = Convert.ToInt32(date2.Subtract(date1).TotalDays) + 1;

            count -= GetNetDynamicNonWorkingDays(date1, date2);
            count -= CountWeekDaysBetween(FixedNonSet, date1, date2);

            return count;
        }

        protected static int CountWeekDaysBetween(ICollection<DayOfWeek> days, DateTime date1, DateTime date2)
        {
            date1 = date1.Date;
            date2 = date2.Date;

            int start = (int)date1.DayOfWeek;
            int count = Convert.ToInt32(date2.Subtract(date1).TotalDays) + start + 1;

            int rema;
            int quot = Math.DivRem(count, 7, out rema);

            int res = quot * days.Count;

            foreach (var day in days)
            {
                if (start > (int)day) res--;
                if (rema > (int)day) res++;
            }

            return res;
        }

        #endregion
    }
}


