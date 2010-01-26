using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Simple.Services.Remoting.Serialization
{
	public class CustomBinaryServerFormatterSinkProvider: IServerFormatterSinkProvider 
	{
		private IServerChannelSinkProvider nextProvider;

		public CustomBinaryServerFormatterSinkProvider() {}
		public CustomBinaryServerFormatterSinkProvider(IDictionary properties, ICollection providerData) {}
		
		public IServerChannelSink CreateSink(IChannelReceiver channel)
		{
			if (channel == null) throw new ArgumentNullException("channel");

			return new CustomBinaryServerFormatterSink(nextProvider.CreateSink(channel), channel);
		}

		public IServerChannelSinkProvider Next
		{
			get { return nextProvider; }
			set { nextProvider = value; }
		}

		public void GetChannelData(IChannelDataStore channelData)
		{
		}
		
	}
}
