using System;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting;
using System.ComponentModel;
using Simple.Client;

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

        public IChannelReceiver GetChannel()
        {
            Uri uri = GetUriFromAddressBase();

            Simply.Do.Log(this).DebugFormat("Creating channel for URI {0}...", uri);

            System.Collections.IDictionary dict = new System.Collections.Hashtable();
            dict["port"] = uri.Port;
            dict["name"] = null;

            switch (uri.Scheme.ToLower())
            {
                case "tcp":
                    return new TcpChannel(dict, null, null);
                case "http":
                    return new HttpChannel(dict, null, null);
                default:
                    throw new ArgumentException("Invalid scheme: " + uri.Scheme);
            }
        }
    }
}
