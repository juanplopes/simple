using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleLibrary.Rules;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface;

namespace Sample.BusinessServer.Infra
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        #region IExceptionHandler Members

        public bool Handle(Exception e)
        {
            CustomFault test = new CustomFault(CustomFault.CustomFaultType.Test2);
            test.Throw();
            return true;
        }

        #endregion
    }
}
