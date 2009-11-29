using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Services
{
    public interface ICallHeadersHandler
    {
        void InjectCallHeaders(object target, MethodBase method, object[] args);
        void RecoverCallHeaders(object target, MethodBase method, object[] args);
    }

    public class NullCallHeadersHandler : ICallHeadersHandler
    {
        public void InjectCallHeaders(object target, MethodBase method, object[] args)
        {
        }

        public void RecoverCallHeaders(object target, MethodBase method, object[] args)
        {
        }
    }
}
