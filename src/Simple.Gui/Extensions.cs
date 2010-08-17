using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple.Gui
{
    public static class Extensions
    {
        public static void InvokeControlAction<T>(this T cont, Action<T> action) where T : Control
        {
            if (cont.InvokeRequired)
            {
                cont.Invoke(new Action<T, Action<T>>(InvokeControlAction),
                          new object[] { cont, action });
            }
            else
            {
                action(cont);
            }
        }
    }
}
