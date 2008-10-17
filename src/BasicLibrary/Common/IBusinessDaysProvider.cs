using System;
using System.Collections.Generic;
using System.Text;

namespace BasicLibrary.Common
{
    public interface IBusinessDaysProvider
    {
        bool IsBusinessDay(DateTime pobjDate);
    }
}
