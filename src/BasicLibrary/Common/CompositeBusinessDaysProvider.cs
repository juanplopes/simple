using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Common
{
    public class CompositeBusinessDaysProvider : IBusinessDaysProvider
    {
        protected IBusinessDaysProvider[] _providers;
        public CompositeBusinessDaysProvider(params IBusinessDaysProvider[] providers)
        {
            _providers = providers;
        }

        #region IBusinessDaysProvider Members

        public bool IsBusinessDay(DateTime pobjDate)
        {
            foreach (var provider in _providers)
            {
                if (!provider.IsBusinessDay(pobjDate)) return false;
            }
            return true;
        }

        #endregion
    }
}
