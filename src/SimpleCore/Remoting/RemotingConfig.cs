using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ServiceModel.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting;
using System.ComponentModel;

namespace Simple.Remoting
{
    public enum HostingTypeEnum
    {
        TCP,
        HTTP
    }

    [XmlRoot("remoting")]
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

            switch (uri.Scheme.ToLower())
            {
                case "tcp":
                    return new TcpChannel(uri.Port);
                case "http":
                    return new HttpChannel(uri.Port);
                default:
                    throw new ArgumentException("Invalid scheme: " + uri.Scheme);
            }
        }
    }
}
