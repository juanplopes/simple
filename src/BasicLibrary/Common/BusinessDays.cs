using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Common
{
    public class BusinessDays
    {
        private static IDictionary<IBusinessDaysProvider, BusinessDays> instances;
        public static BusinessDays Get(IBusinessDaysProvider pobjProvider)
        {
            try
            {
                return instances[pobjProvider];
            }
            catch (KeyNotFoundException)
            {
                return instances[pobjProvider] = new BusinessDays(pobjProvider);
            }
        }
        static BusinessDays()
        {
            instances = new Dictionary<IBusinessDaysProvider, BusinessDays>();
        }

        protected DateTime referenceDate;
        protected IList<DateTime> forwardDays;
        protected IList<DateTime> backwardDays;
        protected IDictionary<DateTime, int> refCache;
        protected IBusinessDaysProvider provider;

        protected BusinessDays(IBusinessDaysProvider provider)
        {
            this.forwardDays = new List<DateTime>();
            this.backwardDays = new List<DateTime>();
            this.refCache = new Dictionary<DateTime, int>();
            this.referenceDate = DateTime.Now.Date;
            this.provider = provider;
            this.refCache[referenceDate] = 0;
        }

        public bool IsBusinessDay(DateTime date)
        {
            return provider.IsBusinessDay(date);
        }

        public int GetBetween(DateTime date1, DateTime date2)
        {
            int lintDate1 = GetReferenceOffset(date1),
                lintDate2 = GetReferenceOffset(date2);
            return Math.Abs(lintDate2 - lintDate1);
        }

        public DateTime GetBackwards(int businessDays)
        {
            return GetBackwards(businessDays, DateTime.Now);
        }

        public DateTime GetInAdvance(int businessDays)
        {
            return GetInAdvance(businessDays, DateTime.Now);
        }

        public DateTime GetBackwards(int businessDays, DateTime reference)
        {
            int offset = GetReferenceOffset(reference);
            return GetGeneric(-businessDays + offset);
        }

        public DateTime GetInAdvance(int businessDays, DateTime reference)
        {
            int offset = GetReferenceOffset(reference);
            return GetGeneric(businessDays + offset);
        }

        protected int GetReferenceOffset(DateTime date)
        {
            date = date.Date;
            if (date == referenceDate) return 0;

            if (!refCache.ContainsKey(date))
            {
                IList<DateTime> listOfDays = (date < referenceDate) ? backwardDays : forwardDays;
                int direction = (date < referenceDate) ? -1 : 1;

                DateTime current = listOfDays.Count > 0 ? listOfDays[listOfDays.Count - 1] : referenceDate;

                while (date.CompareTo(current) == direction)
                {
                    current = current.AddDays(direction);
                    if (IsBusinessDay(current)) listOfDays.Add(current);
                    refCache[current] = direction * listOfDays.Count;
                }
            }

            return refCache[date];
        }

        protected DateTime GetGeneric(int businessDays)
        {
            if (businessDays == 0) return referenceDate;

            IList<DateTime> listOfDays = (businessDays < 0) ? backwardDays : forwardDays;
            int direction = (businessDays < 0) ? -1 : 1;
            businessDays = Math.Abs(businessDays);

            DateTime lobjCurrent = listOfDays.Count > 0 ? listOfDays[listOfDays.Count - 1] : referenceDate;

            while (businessDays > listOfDays.Count)
            {
                lobjCurrent = lobjCurrent.AddDays(direction);
                if (IsBusinessDay(lobjCurrent)) listOfDays.Add(lobjCurrent);
                refCache[lobjCurrent] = direction * listOfDays.Count;
            }

            return listOfDays[businessDays - 1];
        }
    }
}
