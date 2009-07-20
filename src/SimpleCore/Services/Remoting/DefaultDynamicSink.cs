using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

namespace Simple.Services.Remoting
{
    public class DefaultDynamicSink : IMessageSink
    {
        #region IMessageSink Members

        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            throw new NotImplementedException();
        }

        public MarshalByRefObject Obj { get; set; }
        public IMessageSink NextSink { get; set; }

        public DefaultDynamicSink(MarshalByRefObject obj, IMessageSink sink)
        {
            Obj = obj;
            NextSink = sink;
        }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            return msg;
        }

        #endregion
    }
}
