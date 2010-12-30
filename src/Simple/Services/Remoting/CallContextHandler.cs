using System.Runtime.Remoting.Messaging;
using System;

namespace Simple.Services.Remoting
{
    public class CallContextHandler : IContextHandler
    {
        static string guid = typeof(CallContextHandler).GUID.ToString();

        public void InjectCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            CallContext.LogicalSetData(guid, SimpleContext.Get());
        }

        public void RecoverCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            object obj = CallContext.LogicalGetData(guid);
            SimpleContext.Force((obj as SimpleContext) ?? new SimpleContext());
            //CallContext.FreeNamedDataSlot(guid);
        }

    }
}
