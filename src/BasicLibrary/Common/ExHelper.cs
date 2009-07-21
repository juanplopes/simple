using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using log4net;

namespace BasicLibrary.Common
{
    public static class ExHelper
    {
        public static Exception ForReal(Exception e)
        {
            while (e is TargetInvocationException) e = e.InnerException;
            return e;
        }

        public static void LogAll(ILog log, Exception e)
        {
            for (int nivel = 0; e != null; nivel++ )
            {
                if (nivel == 0)
                    log.ErrorFormat("Error: {0}", e.Message);
                else
                    log.WarnFormat("Error: {0}", e.Message);

                log.DebugFormat("Exception", e);
            }
        }
    }
}
