using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Simple.Services.Remoting.Serialization;
using Simple.IO.Serialization;

namespace Simple.Services.Remoting.Serialization
{
	public class CustomBinaryServerFormatterSink: BaseChannelSinkWithProperties, IServerChannelSink
	{

		private IServerChannelSink nextSink;

		public CustomBinaryServerFormatterSink(IServerChannelSink nextSink, IChannelReceiver receiver)
		{
			if (receiver == null) throw new ArgumentNullException("receiver");
			this.nextSink = nextSink;
		}

		public void AsyncProcessResponse(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, 
		                                 ITransportHeaders headers, Stream stream)
		{
			SerializeResponse(sinkStack, msg, ref headers, out stream);
			sinkStack.AsyncProcessResponse(msg, headers, stream);
		}

		public Stream GetResponseStream(IServerResponseChannelSinkStack sinkStack, object state, IMessage msg, ITransportHeaders headers)
		{
			throw new NotSupportedException();
		}

		public ServerProcessing ProcessMessage(IServerChannelSinkStack sinkStack, IMessage requestMsg, ITransportHeaders requestHeaders, Stream requestStream, out IMessage responseMsg, out ITransportHeaders responseHeaders, out Stream responseStream)
		{
			// reversed engineered from .NET framework class
			if (requestMsg != null)
			{
				return nextSink.ProcessMessage(sinkStack, requestMsg, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);
			}

			if (requestHeaders == null) throw new ArgumentNullException("requestHeaders");

			responseHeaders = null;
			responseStream = null;
			responseMsg = null;

			string contentType = (string) requestHeaders["Content-Type"];
			string requestVerb = (string) requestHeaders["__RequestVerb"];

			if (contentType != "application/octet-stream" || (requestVerb != "POST" && requestVerb != "M-POST"))
			{
				if (nextSink == null)
				{
					responseHeaders = new TransportHeaders();
					responseHeaders["__HttpStatusCode"] = "400";
					responseHeaders["__HttpReasonPhrase"] = "Bad Request";
					return ServerProcessing.Complete;
				}

				return nextSink.ProcessMessage(sinkStack, null, requestHeaders, requestStream, out responseMsg, out responseHeaders, out responseStream);
			}
			
			object customErrors = requestHeaders["__CustomErrorsEnabled"];
			if (customErrors != null && customErrors is bool)
			{
				CallContext.SetData("__CustomErrorsEnabled", (bool)customErrors);
			}

			try
			{
				if (RemotingServices.GetServerTypeForUri((string)requestHeaders["__RequestUri"]) == null)
				{
					throw new RemotingException("Remoting Channel Sink UriNotPublished");
				}

				requestMsg = DeserializeBinaryRequestMessage(requestStream, requestHeaders);
				
				if (requestMsg == null)
				{
					throw new RemotingException("Remoting Deserialize Error");
				}
				sinkStack.Push(this, null);

				ServerProcessing processing = nextSink.ProcessMessage(sinkStack, requestMsg, requestHeaders, null, out responseMsg, out responseHeaders, out responseStream);

				if (responseStream != null)
				{
					throw new RemotingException("Remoting_ChnlSink_WantNullResponseStream");
				}

				switch (processing)
				{
					case ServerProcessing.Complete:
					{
						if (responseMsg == null)
						{
							throw new RemotingException("Remoting_DispatchMessage");
						}
						sinkStack.Pop(this);

						SerializeResponse(sinkStack, responseMsg, ref responseHeaders, out responseStream);

						return processing;
					}
					case ServerProcessing.OneWay:
					{
						sinkStack.Pop(this);
						return processing;
					}
					case ServerProcessing.Async:
					{
						sinkStack.Store(this, null);
						return processing;
					}
				}

				return processing;
			}
			catch (Exception ex)
			{
				responseMsg = new ReturnMessage(ex, requestMsg == null ? null : (IMethodCallMessage)requestMsg);

				CallContext.SetData("__ClientIsClr", 1);
				responseStream = new MemoryStream();
				CreateFormatter(true).Serialize(responseStream, responseMsg, null);
				CallContext.FreeNamedDataSlot("__ClientIsClr");
				responseStream.Position = 0;
				responseHeaders = new TransportHeaders();
				responseHeaders["Content-Type"] = "application/octet-stream";
				return ServerProcessing.Complete;
			}
			finally
			{
				CallContext.FreeNamedDataSlot("__CustomErrorsEnabled");
			}
		}

		private void SerializeResponse(IServerResponseChannelSinkStack sinkStack, IMessage msg, ref ITransportHeaders headers, out Stream stream)
		{
			bool usingLocallyCreatedMemoryStream = false;
			
			// Does this section do anything useful since we can't use BaseTransportHeaders??
			TransportHeaders transportHeaders = new TransportHeaders();
			if(headers != null) {
				foreach(DictionaryEntry entry in headers) {
					transportHeaders[entry.Key] = entry.Value;
				}
			}
			headers = transportHeaders;
			
			// Request a stream into which the serialized message will go
			stream = sinkStack.GetResponseStream(msg, headers);
			
			// None found so create a memory stream (would normally be a ChunkedMemoryStream but that is inaccessible)
			if(stream == null) {
				stream = new MemoryStream();
				usingLocallyCreatedMemoryStream = true;
			}

			// Serialize the message - normally done by CoreChannel.SerializeBinaryMessage(msg, stream, this._includeVersioning);
			CreateFormatter(true).Serialize(stream, msg, null);

			// Rewind the stream if ours
			if(usingLocallyCreatedMemoryStream) stream.Position = 0;
		}

		public IServerChannelSink NextChannelSink
		{
			get { return nextSink; }
		}

		
		// Our equivalent of CoreChannel.DeserializeBinaryRequestMessage()
		private IMessage DeserializeBinaryRequestMessage(Stream requestStream, ITransportHeaders requestHeaders)
		{
			Stream streamToDeserialize = requestStream;
			try
			{
				IMessage result = (IMessage)
					CreateFormatter(false).DeserializeMethodResponse( 
						streamToDeserialize, new HeaderHandler(new UriHeaderHandler(requestHeaders).HeaderHandler), null);

				return result;
			}
			finally
			{
				streamToDeserialize.Close();
			}
		}


		protected BinaryFormatter CreateFormatter(bool serialize)
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
		
		private class UriHeaderHandler
		{
			string uri;

			public UriHeaderHandler(ITransportHeaders headers)
			{
				uri = (string) headers["__RequestUri"];
			}

			public object HeaderHandler(Header[] headers)
			{
				return uri;
			}
		}
	}
}
