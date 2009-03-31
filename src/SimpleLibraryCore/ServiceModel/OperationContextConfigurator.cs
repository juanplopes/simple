using System;
using System.Collections.Generic;

using System.Text;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using SimpleLibrary.Config;
using System.ServiceModel.Channels;
using SimpleLibrary.DataAccess;
using System.Runtime.Serialization;

namespace SimpleLibrary.ServiceModel
{
    public class OperationContextConfigurator : IEndpointConfigurator, IEndpointBehavior, IClientMessageInspector, IDispatchMessageInspector
    {
        #region IEndpointConfigurator Members

        public void Configure(bool isClientSide, System.ServiceModel.Description.ServiceEndpoint endpoint, SimpleLibrary.Config.ConfiguratorElement config)
        {
            endpoint.Behaviors.Remove<OperationContextConfigurator>();
            endpoint.Behaviors.Add(this);
        }

        #endregion


        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(this);
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(this);
        }

        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion

        public void HandleGetContext(ref Message msg)
        {
            try
            {
                SimpleContext context = msg.Headers.GetHeader<SimpleContext>(typeof(SimpleContext).GUID.ToString(), Constants.DefaultNamespace);
                SimpleContext.Set(context);
            }
            catch { }
        }

        public void HandleSetContext(ref Message msg)
        {
            MessageHeader header = MessageHeader.CreateHeader(typeof(SimpleContext).GUID.ToString(), Constants.DefaultNamespace, SimpleContext.Get());
            msg.Headers.Add(header);
        }

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            HandleGetContext(ref reply);
        }

        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
        {
            HandleSetContext(ref request);
            return null;
        }

        #endregion

        #region IDispatchMessageInspector Members

        public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            HandleGetContext(ref request);
            return null;
        }

        public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            HandleSetContext(ref reply);
        }

        #endregion
    }
}
