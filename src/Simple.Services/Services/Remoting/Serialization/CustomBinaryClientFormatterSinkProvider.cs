using System;
using System.Collections;
using System.Runtime.Remoting.Channels;

namespace Simple.Services.Remoting.Serialization
{
	public class CustomBinaryClientFormatterSinkProvider: IClientFormatterSinkProvider
	{
		private IClientChannelSinkProvider nextProvider;
		
		public CustomBinaryClientFormatterSinkProvider() {}
		public CustomBinaryClientFormatterSinkProvider(IDictionary properties, ICollection providerData) {}

		public IClientChannelSink CreateSink(IChannelSender channel, string url, object remoteChannelData)
		{
			IClientChannelSink nextSink = null;
			
			if (nextProvider != null) {
				nextSink = nextProvider.CreateSink(channel, url, remoteChannelData);
				if (nextSink == null) return null;
			}

			return new CustomBinaryClientFormatterSink(nextSink);
		}

		public IClientChannelSinkProvider Next
		{
			get	{ return nextProvider; }
			set { nextProvider = value; }
		}
	}
}
