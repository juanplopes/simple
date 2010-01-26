using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Simple.Services.Remoting.Serialization;
using Simple.IO.Serialization;

namespace Simple.Services.Remoting.Serialization
{
    public class CustomBinaryClientFormatterSink : BaseChannelSinkWithProperties, IClientFormatterSink
    {

        private IClientChannelSink nextSink;

        public CustomBinaryClientFormatterSink(IClientChannelSink nextSink)
        {
            this.nextSink = nextSink;
        }

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
            try
            {
                ITransportHeaders headers;
                Stream stream;
                SerializeRequest(msg, out headers, out stream);
                ClientChannelSinkStack clientChannelSinkStack = new ClientChannelSinkStack(replySink);
                clientChannelSinkStack.Push(this, msg);
                nextSink.AsyncProcessRequest(clientChannelSinkStack, msg, headers, stream);
            }
            catch (Exception ex)
            {
                IMessage returnMessage = new ReturnMessage(ex, methodCallMessage);
                if (replySink == null) return null;
                replySink.SyncProcessMessage(returnMessage);
            }
            return null;
        }

        public void AsyncProcessRequest(IClientChannelSinkStack sinkStack, IMessage msg, ITransportHeaders headers, Stream stream)
        {
            throw new NotSupportedException();
        }

        public void AsyncProcessResponse(IClientResponseChannelSinkStack sinkStack, object state, ITransportHeaders headers, Stream stream)
        {
            IMethodCallMessage methodCallMessage = state as IMethodCallMessage;
            IMessage message = DeserializeResponse(methodCallMessage, headers, stream);
            sinkStack.DispatchReplyMessage(message);
        }

        public Stream GetRequestStream(IMessage msg, ITransportHeaders headers)
        {
            throw new NotSupportedException();
        }

        public void ProcessMessage(IMessage msg, ITransportHeaders requestHeaders, Stream requestStream,
                                   out ITransportHeaders responseHeaders, out Stream responseStream)
        {
            throw new NotSupportedException();
        }

        public IClientChannelSink NextChannelSink
        {
            get { return nextSink; }
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
            IMethodMessage methodMessage = msg as IMethodMessage;

            int numberOfRetries = 5;
            while (true)
            {
                try
                {
                    return SerializeAndProcessMessage(msg, methodCallMessage);
                }
                catch (WebException ex)
                {
                    if (--numberOfRetries == 0 || !CanRetryMethod(methodMessage))
                    {
                        return new ReturnMessage(ex, methodCallMessage);
                    }
                }
                catch (Exception ex)
                {
                    return new ReturnMessage(ex, methodCallMessage);
                }
            }
        }

        private bool CanRetryMethod(IMethodMessage mm)
        {
            ReadOnlyAttribute readOnly = (ReadOnlyAttribute)Attribute.GetCustomAttribute(mm.MethodBase, typeof(ReadOnlyAttribute));
            return readOnly != null && readOnly.IsReadOnly;
        }

        private IMessage SerializeAndProcessMessage(IMessage msg, IMethodCallMessage mcm)
        {
            ITransportHeaders headers;
            Stream stream;
            SerializeRequest(msg, out headers, out stream);

            ITransportHeaders responseHeaders;
            Stream responseStream;

            nextSink.ProcessMessage(msg, headers, stream, out responseHeaders, out responseStream);

            if (responseHeaders == null) throw new ArgumentException("returnHeaders should not be null");

            IMessage retVal = DeserializeResponse(mcm, responseHeaders, responseStream);

            return retVal;
        }

        private void SerializeRequest(IMessage msg, out ITransportHeaders headers, out Stream stream)
        {
            headers = new TransportHeaders();
            headers["Content-Type"] = "application/octet-stream";
            headers["__RequestVerb"] = "POST";

            Boolean usingMemoryStream = false;
            stream = nextSink.GetRequestStream(msg, headers);
            if (stream == null)
            {
                stream = new MemoryStream();
                usingMemoryStream = true;
            }

            CreateFormatter(true).Serialize(stream, msg, null);

            if (usingMemoryStream)
            {
                stream.Position = 0;
            }
        }


        public IMessageSink NextSink
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        private IMessage DeserializeResponse(IMethodCallMessage mcm, ITransportHeaders headers, Stream stream)
        {
            try
            {
                if (headers["Content-Type"] != null && ((string)headers["Content-Type"]).StartsWith("text/plain"))
                {
                    throw new InvalidOperationException(new StreamReader(stream).ReadToEnd());
                }

                return (IMessage)CreateFormatter(false).DeserializeMethodResponse(stream, null, mcm);
            }
            finally
            {
                stream.Close();
            }
        }

        private BinaryFormatter CreateFormatter(bool serialize)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Context = new StreamingContext(StreamingContextStates.Other);
            formatter.AssemblyFormat = FormatterAssemblyStyle.Full;

            if (!serialize)
                formatter.SurrogateSelector = new ExpressionSurrogateSelector();
            else
            {
                ISurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
                surrogateSelector.ChainSelector(new ExpressionSurrogateSelector());
                formatter.SurrogateSelector = surrogateSelector;
            }
            return formatter;
        }
    }
}
