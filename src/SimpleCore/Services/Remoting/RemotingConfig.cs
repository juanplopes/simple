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
        public const string DefaultRemotingUrl = "activator";

        [XmlElement("baseAddress")]
        public string BaseAddress { get; set; }


        public Uri GetUriFromAddressBase()
        {
            return new Uri(BaseAddress, UriKind.Absolute);
        }

        public IChannelReceiver GetServerChannel()
        {
            Uri uri = GetUriFromAddressBase();

            Simply.Do.Log(this).DebugFormat("Creating SERVER channel for URI {0}...", uri);

            switch (uri.Scheme.ToLower())
            {
                case "tcp":
                    return new TcpServerChannel(null, uri.Port);
                case "http":
                    return new HttpServerChannel(null, uri.Port);
                default:
                    throw new ArgumentException("Invalid scheme: " + uri.Scheme);
            }
        }

        public string GetChannelName()
        {
            return GetUriFromAddressBase().Scheme.ToLower();
        }

        public IChannelSender GetClientChannel()
        {
            Uri uri = GetUriFromAddressBase();

            Simply.Do.Log(this).DebugFormat("Creating CLIENT channel for URI {0}...", uri);

            switch (uri.Scheme.ToLower())
            {
                case "tcp":
                    return new TcpClientChannel();
                case "http":
                    return new HttpClientChannel();
                default:
                    throw new ArgumentException("Invalid scheme: " + uri.Scheme);
            }
        }
    }
}
