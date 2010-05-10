using System;
using System.Runtime.Remoting.Channels;

namespace Simple.Services.Remoting.Sinks
{
    public class CallHeadersSink : BaseChannelSinkWithProperties, IClientChannelSink
    {
        #region IClientChannelSink Members

        public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, System.Runtime.Remoting.Messaging.IMessage msg, ITransportHeaders headers, System.IO.Stream stream)
        {
        }

        public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, System.IO.Stream stream)
        {
        }

        public System.IO.Stream GetRequestStream(System.Runtime.Remoting.Messaging.IMessage msg, ITransportHeaders headers)
        {
            return NextChannelSink.GetRequestStream(msg, headers);
        }

        public IClientChannelSink NextChannelSink
        {
            get { throw new NotImplementedException(); }
        }

        public void ProcessMessage(System.Runtime.Remoting.Messaging.IMessage msg, ITransportHeaders requestHeaders, System.IO.Stream requestStream, out ITransportHeaders responseHeaders, out System.IO.Stream responseStream)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
