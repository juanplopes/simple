using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Common
{
    public class WorkingDays : IWorkingDaysProvider
    {
        private static IDictionary<IWorkingDaysProvider, WorkingDays> instances;
        public static WorkingDays Get(IWorkingDaysProvider pobjProvider)
        {
            try
            {
                return instances[pobjProvider];
            }
            catch (KeyNotFoundException)
            {
                return instances[pobjProvider] = new WorkingDays(pobjProvider);
            }
        }
        static WorkingDays()
        {
            instances = new Dictionary<IWorkingDaysProvider, WorkingDays>();
        }


        public WorkingDays(IWorkingDaysProvider provider)
        {
            this.provider = provider;
        }

        protected IWorkingDaysProvider provider;

        public bool IsWorkingDay(DateTime date)
        {
            return provider.IsWorkingDay(date);
        }
        public int GetNetWorkingDays(DateTime date1, DateTime date2)
        {
            return provider.GetNetWorkingDays(date1, date2);
        }

        protected DateTime BinarySearch(bool direction, int businessDays, DateTime reference, bool considerCurrent)
        {
            int mult = (direction ? 1 : -1);

            reference = reference.Date.AddDays(considerCurrent ? 0 : 1 * mult);
            long start = reference.Ticks;
            long end = reference.AddDays(businessDays * 2 * mult).Ticks;

            if (start > end) MathHelper.Swap(ref start, ref end);

            long mid = 0;
            DateTime date = default(DateTime);
            int value = 0;

            if (direction)
                while (GetNetWorkingDays(reference, new DateTime(end)) < businessDays)
                    end += (end - start);
            else
                while (GetNetWorkingDays(new DateTime(start), reference) < businessDays)
                    start -= (end - start);

            while (true)
            {
                mid = (start + end) / 2;
                date = new DateTime(mid);
                if (direction)
                    value = GetNetWorkingDays(reference, date);
                else
                    value = GetNetWorkingDays(date, reference);

                if (value == businessDays)
                    break;
                else
                {
                    if (value > businessDays == direction)
                        end = mid;
                    else if (value < businessDays == direction)
                        start = mid;
                }
            }

            while (!IsWorkingDay(date))
                date.AddDays(-1);

            return date;
        }

        public DateTime GetInAdvance(int businessDays, DateTime reference, bool considerCurrent)
        {
            return BinarySearch(true, businessDays, reference, considerCurrent);
        }

        public DateTime GetBackwards(int businessDays, DateTime reference, bool considerCurrent)
        {
            return BinarySearch(false, businessDays, reference, considerCurrent);
        }


    }
}
