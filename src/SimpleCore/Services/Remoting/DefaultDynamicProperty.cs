using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Contexts;

namespace Simple.Services.Remoting
{
    public class DefaultDynamicProperty : IDynamicProperty, IContributeObjectSink
    {
        public static string PropertyName = "ASDASDASDASDDSA";

        public DefaultDynamicProperty() { }
        public DefaultDynamicProperty(MarshalByRefObject obj, Context ctx)
        {
        }

        #region IDynamicProperty Members

        public string Name
        {
            get { return PropertyName; }
        }

        #endregion



        #region IContributeObjectSink Members

        public System.Runtime.Remoting.Messaging.IMessageSink GetObjectSink(MarshalByRefObject obj, System.Runtime.Remoting.Messaging.IMessageSink nextSink)
        {
            return new DefaultDynamicSink(obj, nextSink);
        }

        #endregion
    }
}
