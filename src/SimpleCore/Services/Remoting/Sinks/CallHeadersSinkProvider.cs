using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Channels;

namespace Simple.Services.Remoting.Sinks
{
    public class CallHeadersSinkProvider : IClientChannelSinkProvider
    {
        #region IClientChannelSinkProvider Members

        public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
        {
            throw new NotImplementedException();
        }

        public IClientChannelSinkProvider Next
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
