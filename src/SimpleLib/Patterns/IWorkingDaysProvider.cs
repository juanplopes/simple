using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Patterns
{
    public interface IWorkingDaysProvider
    {
        bool IsWorkingDay(DateTime date);
        int GetNetWorkingDays(DateTime date1, DateTime date2);
    }
}
