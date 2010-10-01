using System.Reflection;

namespace Simple.Services
{
    public interface IContextHandler
    {
        void InjectCallHeaders(object target, MethodBase method, object[] args);
        void RecoverCallHeaders(object target, MethodBase method, object[] args);
    }

    public class NullContextHandler : IContextHandler
    {
        public void InjectCallHeaders(object target, MethodBase method, object[] args)
        {
        }

        public void RecoverCallHeaders(object target, MethodBase method, object[] args)
        {
        }
    }
}
