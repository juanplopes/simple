using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple.Reflection;

namespace Simple.Services
{
    public interface IInterceptor
    {
        object Intercept(object target, MethodBase method, object[] args);
    }
}
