using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Patterns;
using System.Reflection;

namespace Simple
{
    public static class DisposableExtensions
    {
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
            catch { Simply.Do.Log(MethodBase.GetCurrentMethod()).WarnFormat("Error disposing {0}", disposable); }
        }
    }
}
