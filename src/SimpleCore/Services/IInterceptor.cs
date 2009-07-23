using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Services
{
    public interface IInterceptor
    {
        object Intercept(object obj, MethodBase info, object[] parameters);
    }
}
