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

        private DateTime referenceDate;
        private IList<DateTime> forwardDays;
        private IList<DateTime> backwardDays;
        private IDictionary<DateTime, int> refCache;
        private IBusinessDaysProvider provider;

        private BusinessDays(IBusinessDaysProvider pobjProvider)
        {
            forwardDays = new List<DateTime>();
            backwardDays = new List<DateTime>();
            refCache = new Dictionary<DateTime, int>();
            referenceDate = DateTime.Now.Date;
            provider = pobjProvider;
            refCache[referenceDate] = 0;
        }

        public bool IsBusinessDay(DateTime pobjDate)
        {
            return provider.IsBusinessDay(pobjDate);
        }

        public int GetBetween(DateTime pobjDate1, DateTime pobjDate2)
        {
            int lintDate1 = GetReferenceOffset(pobjDate1),
                lintDate2 = GetReferenceOffset(pobjDate2);
            return Math.Abs(lintDate2 - lintDate1);
        }

        public DateTime GetBackwards(int pintBusinessDays)
        {
            return GetBackwards(pintBusinessDays, DateTime.Now);
        }

        public DateTime GetInAdvance(int pintBusinessDays)
        {
            return GetInAdvance(pintBusinessDays, DateTime.Now);
        }

        public DateTime GetBackwards(int pintBusinessDays, DateTime pobjReference)
        {
            int lintOffset = GetReferenceOffset(pobjReference);
            return GetGeneric(-pintBusinessDays + lintOffset);
        }

        public DateTime GetInAdvance(int pintBusinessDays, DateTime pobjReference)
        {
            int lintOffset = GetReferenceOffset(pobjReference);
            return GetGeneric(pintBusinessDays + lintOffset);
        }

        private int GetReferenceOffset(DateTime pobjDate)
        {
            pobjDate = pobjDate.Date;
            if (pobjDate == referenceDate) return 0;

            if (!refCache.ContainsKey(pobjDate))
            {
                IList<DateTime> llstBDays = (pobjDate < referenceDate) ? backwardDays : forwardDays;
                int lintDirection = (pobjDate < referenceDate) ? -1 : 1;

                DateTime lobjCurrent = llstBDays.Count > 0 ? llstBDays[llstBDays.Count - 1] : referenceDate;

                while (pobjDate.CompareTo(lobjCurrent) == lintDirection)
                {
                    lobjCurrent = lobjCurrent.AddDays(lintDirection);
                    if (IsBusinessDay(lobjCurrent)) llstBDays.Add(lobjCurrent);
                    refCache[lobjCurrent] = lintDirection * llstBDays.Count;
                }
            }

            return refCache[pobjDate];
        }

        private DateTime GetGeneric(int pintBusinessDays)
        {
            if (pintBusinessDays == 0) return referenceDate;

            IList<DateTime> llstBDays = (pintBusinessDays < 0) ? backwardDays : forwardDays;
            int lintDirection = (pintBusinessDays < 0) ? -1 : 1;
            pintBusinessDays = Math.Abs(pintBusinessDays);

            DateTime lobjCurrent = llstBDays.Count > 0 ? llstBDays[llstBDays.Count - 1] : referenceDate;

            while (pintBusinessDays > llstBDays.Count)
            {
                lobjCurrent = lobjCurrent.AddDays(lintDirection);
                if (IsBusinessDay(lobjCurrent)) llstBDays.Add(lobjCurrent);
                refCache[lobjCurrent] = lintDirection * llstBDays.Count;
            }

            return llstBDays[pintBusinessDays - 1];
        }
    }
}
