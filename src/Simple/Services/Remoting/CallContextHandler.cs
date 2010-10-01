using System.Runtime.Remoting.Messaging;

namespace Simple.Services.Remoting
{
    public class CallContextHandler : IContextHandler
    {
        public void InjectCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            CallContext.LogicalSetData(this.GetType().GUID.ToString(), SimpleContext.Get());
        }

        public void RecoverCallHeaders(object target, System.Reflection.MethodBase method, object[] args)
        {
            object obj = CallContext.LogicalGetData(this.GetType().GUID.ToString());
            SimpleContext.Force((obj as SimpleContext) ?? new SimpleContext());
            CallContext.FreeNamedDataSlot(this.GetType().GUID.ToString());
        }

    }
}
