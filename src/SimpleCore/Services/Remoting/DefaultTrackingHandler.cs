using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Services;
using System.Runtime.Remoting.Contexts;

namespace Simple.Services.Remoting
{
    public class DefaultTrackingHandler : ITrackingHandler
    {
        public static void EnsureRegistered()
        {
            var list = TrackingServices.RegisteredHandlers;
            lock (list)
            {
                foreach (ITrackingHandler handler in list)
                {
                    if (handler is DefaultTrackingHandler)
                        return;
                }

                TrackingServices.RegisterTrackingHandler(new DefaultTrackingHandler());
                Context.RegisterDynamicProperty(new DefaultDynamicProperty(), null, Context.DefaultContext);
            }
        }

        #region ITrackingHandler Members

        public void DisconnectedObject(object obj)
        {
        }

        public void MarshaledObject(object obj, System.Runtime.Remoting.ObjRef or)
        {
        }

        public void UnmarshaledObject(object obj, System.Runtime.Remoting.ObjRef or)
        {
        }

        #endregion
    }
}
