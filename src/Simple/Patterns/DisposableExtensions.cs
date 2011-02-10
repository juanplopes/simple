using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using System.Reflection;
using log4net;

namespace Simple
{
    public static class DisposableExtensions
    {
        static ILog logger = Simply.Do.Log(MethodBase.GetCurrentMethod());
        public static IDisposable ComposeWith(this IDisposable first, params IDisposable[] disposables)
        {
            return new DisposableAction(() =>
            {
                foreach (var disposable in disposables.Reverse())
                {
                    TryToDispose(disposable);
                }
                TryToDispose(first);
            });
        }

        private static void TryToDispose(IDisposable disposable)
        {
            try { disposable.Dispose(); }
            catch (Exception e)
            {
                logger
                    .Warn("Error disposing {0}".AsFormatFor(disposable), e);
            }
        }
    }
}
