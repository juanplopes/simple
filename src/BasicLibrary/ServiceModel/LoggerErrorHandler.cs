using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;
using BasicLibrary.Logging;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace BasicLibrary.ServiceModel
{
    public class LoggerErrorHandler : IErrorHandler
    {
        #region IErrorHandler Members

        public bool HandleError(Exception error)
        {
            MainLogger.Default.Error(error.Message, error);
            return false;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
        }

        #endregion
    }
}
