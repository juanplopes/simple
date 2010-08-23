using System;
using System.ComponentModel;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Xml;
using System.Xml.Serialization;

namespace Simple.Services.Remoting
{
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

        [XmlElement("localOnly"), DefaultValue(false)]
        public bool LocalOnly { get; set; }

        public string GetEndpointKey(Type type)
        {
            string mask = EndpointMask ?? MaskVariable;
            return mask.Replace(MaskVariable, TypesHelper.GetFlatClassName(type));
        }

        public Uri GetUriFromAddressBase()
        {
            return new Uri(BaseAddress, UriKind.Absolute);
        }

        public IChannelReceiver GetChannel()
        {
            Uri uri = GetUriFromAddressBase();
            Simply.Do.Log(this).DebugFormat("Creating channel for URI {0}...", uri);
            return ChannelSelector.Do.GetHandler(uri).CreateServerChannel(null, uri);
        }

        public void TryRegisterClientChannel()
        {
            Uri uri = GetUriFromAddressBase();
            Simply.Do.Log(this).DebugFormat("Creating client channel for URI {0}...", uri);
            var handler = ChannelSelector.Do.GetHandler(uri);
            if (ChannelServices.GetChannel(handler.DefaultName) == null)
                ChannelServices.RegisterChannel(handler.CreateClientChannel(), false);
        }
    }
}
