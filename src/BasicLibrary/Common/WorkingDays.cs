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

            reference = reference.Date.AddDays(considerCurrent && IsWorkingDay(reference) ? 0 : 1 * mult);
            long referenceTicks = reference.Ticks;

            int start = 0;
            int end = businessDays * 2;
            int mid = 0;
            int value = 0;
            DateTime midDate = default(DateTime);


            if (direction)
                while (GetNetWorkingDays(reference,
                  new DateTime(referenceTicks + end * TimeSpan.TicksPerDay)) < businessDays)
                    end = end * 2;
            else
                while (GetNetWorkingDays(new DateTime(referenceTicks - end * TimeSpan.TicksPerDay),
                    reference) < businessDays)
                    end = end * 2;

            while (true)
            {
                mid = (start + end) / 2;
                midDate = new DateTime(referenceTicks + mid * TimeSpan.TicksPerDay * mult);

                if (direction)
                    value = GetNetWorkingDays(reference, midDate);
                else
                    value = GetNetWorkingDays(midDate, reference);

                if (value > businessDays)
                    end = mid - 1;
                else if (value < businessDays)
                    start = mid + 1;
                else
                    break;

            }

            while (!IsWorkingDay(midDate))
                midDate = midDate.AddDays(-mult);

            return midDate.Date;
        }

        public DateTime GetInAdvance(int businessDays, DateTime reference, bool considerCurrent)
        {
            return BinarySearch(true, businessDays, reference, considerCurrent);
        }

        public DateTime GetBackwards(int businessDays, DateTime reference, bool considerCurrent)
        {
            return BinarySearch(false, businessDays, reference, considerCurrent);
        }

        public DateTime GetDays(int businessDays, DateTime reference, bool considerCurrent)
        {
            if (businessDays < 0)
                return GetBackwards(-businessDays, reference, considerCurrent);
            else
                return GetInAdvance(businessDays, reference, considerCurrent);
        }

    }
}
