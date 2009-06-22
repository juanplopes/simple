using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Simple.Common
{
    public static class ExceptionHelper
    {
        public static Exception ForReal(Exception e)
        {
            while (e is TargetInvocationException) e = e.InnerException;
            return e;
        }
    }
}
