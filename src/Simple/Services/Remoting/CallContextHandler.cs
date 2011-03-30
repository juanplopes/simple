using System.Runtime.Remoting.Messaging;
using System;

namespace Simple.Services.Remoting
{
    public class CallContextHandler : IContextHandler
    {
        static string guid = typeof(CallContextHandler).GUID.ToString();

        public void InjectCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            if (SimpleContext.Exists())
                CallContext.LogicalSetData(guid, SimpleContext.Get());
        }

        public void RecoverCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            object obj = CallContext.LogicalGetData(guid);
            if (obj != null)
                SimpleContext.Force(obj as SimpleContext);
            //CallContext.FreeNamedDataSlot(guid);
        }

    }
}
