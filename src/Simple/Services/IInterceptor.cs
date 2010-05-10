using System.Reflection;

namespace Simple.Services
{
    public interface IInterceptor
    {
        object Intercept(object target, MethodBase method, object[] args);
    }
}
