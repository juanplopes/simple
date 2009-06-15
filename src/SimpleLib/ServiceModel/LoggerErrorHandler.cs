using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Channels;
using log4net;
using System.Reflection;
using Simple.Logging;

namespace Simple.ServiceModel
{
    public class LoggerErrorHandler : IErrorHandler
    {
        protected static ILog Logger = SimpleLogger.Get(MethodInfo.GetCurrentMethod().DeclaringType);

        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            Logger.Error(error.Message, error);
            return false;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
        }

        #endregion
    }
}
