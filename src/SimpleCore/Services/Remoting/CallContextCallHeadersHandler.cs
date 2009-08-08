using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Remoting.Messaging;
using System.Collections;

namespace Simple.Services.Remoting
{
    public class CallContextCallHeadersHandler : ICallHeadersHandler
    {
        public void InjectCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            CallContext.SetData(this.GetType().GUID.ToString(), CallHeaders.Do.Data);
        }

        public void RecoverCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            object obj = CallContext.GetData(this.GetType().GUID.ToString());
            CallHeaders.Do.Force((obj as Hashtable) ?? new Hashtable());
            CallContext.FreeNamedDataSlot(this.GetType().GUID.ToString());
        }

    }
}
