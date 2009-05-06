using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace Simple.ServiceModel
{
    // This attribute can be used to install a custom error handler for a service.
    public class ErrorBehaviorExtension : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(ErrorServiceBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ErrorServiceBehavior();
        }
    }
}
