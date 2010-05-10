using System;
using System.Reflection;

namespace Simple.Services
{
    [Serializable]
    public class CallHookArgs
    {
        public CallHookArgs(object target, MethodBase method, object[] args, bool client)
        {
            Target = target;
            Method = method;
            Args = args;
            Client = client;
        }
        public object Target { get; protected set; }
        public MethodBase Method { get; protected set; }
        public object[] Args { get; protected set; }
        public bool Client { get; set; }
        public object Return { get; set; }
    }

    public interface ICallHook
    {
        void Before();
        void AfterSuccess();
        void Finally();
    }

    public abstract class BaseCallHook : ICallHook
    {
        public CallHookArgs CallArgs { get; set; }
        protected BaseCallHook(CallHookArgs args)
        {
            this.CallArgs = args;
        }

        #region ICallHook Members

        public virtual void Before() { }
        public virtual void AfterSuccess() { }
        public virtual void Finally() { }

        #endregion
    }
}
