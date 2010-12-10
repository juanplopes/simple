using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple.Gui
{
    public static class Extensions
    {
        public static object InvokeControlAction<T>(this T cont, Action<T> action) where T : Control
        {
            return InvokeControlAction(cont, obj => { action(obj); return (object)null; });
        }

        public static TRet InvokeControlAction<T, TRet>(this T cont, Func<T, TRet> action) where T : Control
        {
            if (cont.InvokeRequired)
            {
                return (TRet)cont.Invoke(action, cont);
            }
            else
            {
                return action(cont);
            }
        }
    }
}
