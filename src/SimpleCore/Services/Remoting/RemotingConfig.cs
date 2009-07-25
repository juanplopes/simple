using System;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting;
using System.ComponentModel;

namespace Simple.Services.Remoting
{
    public enum HostingTypeEnum
    {
        TCP,
        HTTP
    }

    [XmlRoot("remoting"), Serializable]
    public class RemotingConfig
    {
        public const string MaskVariable = "$typename";

        [XmlElement("baseAddress")]
        public string BaseAddress { get; set; }

        [XmlElement("endpointMask"), DefaultValue(MaskVariable)]
        public string EndpointMask { get; set; }

        [XmlElement("objectMode"), DefaultValue(WellKnownObjectMode.SingleCall)]
        public WellKnownObjectMode ObjectMode { get; set; }

        public string GetEndpointKey(Type type)
        {
            string mask = EndpointMask ?? MaskVariable;

            return mask.Replace(MaskVariable, type.Name);
        }

        public Uri GetUriFromAddressBase()
        {
            return new Uri(BaseAddress, UriKind.Absolute);
        }

        public IChannelReceiver GetChannel()
        {
            Uri uri = GetUriFromAddressBase();
            Simply.Do.Log(this).DebugFormat("Creating channel for URI {0}...", uri);
            switch (uri.Scheme.ToLower())
            {
                case "tcp":
                    return new TcpServerChannel(null, uri.Port);
                case "http":
                    return new HttpServerChannel(null, uri.Port, new BinaryServerFormatterSinkProvider());
                default:
                    throw new ArgumentException("Invalid scheme: " + uri.Scheme);
            }
        }

        public void TryRegisterClientChannel()
        {
            Uri uri = GetUriFromAddressBase();
            Simply.Do.Log(this).DebugFormat("Creating client channel for URI {0}...", uri);
            IChannelSender sender = null;
            switch (uri.Scheme.ToLower())
            {
                case "tcp":
                    sender = new TcpClientChannel();
                    break;
                case "http":
                    sender = new HttpClientChannel("http", new BinaryClientFormatterSinkProvider());
                    break;
                default:
                    throw new ArgumentException("Invalid scheme: " + uri.Scheme);
            }
            if (ChannelServices.GetChannel(sender.ChannelName) == null)
                ChannelServices.RegisterChannel(sender, false);
        }
    }
}
