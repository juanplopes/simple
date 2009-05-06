using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Reflection;
using System.Collections.Specialized;
using System.Collections;

namespace Simple.UI.Web
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class SaveControlStateAttribute : Attribute { }

    public static class ControlStateHelper
    {
        public static IDictionary SaveControlState(object control)
        {
            if (control == null) return null;

            Hashtable result = new Hashtable();

            foreach (PropertyInfo prop in control.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(SaveControlStateAttribute)))
                {
                    result[prop.Name] = prop.GetValue(control, null);
                }
            }

            return result;
        }

        public static void LoadControlState(object control, IDictionary state)
        {
            if (control == null || state == null) return;

            foreach (PropertyInfo prop in control.GetType().GetProperties())
            {
                if (Attribute.IsDefined(prop, typeof(SaveControlStateAttribute)))
                {
                    try
                    {
                        prop.SetValue(control, state[prop.Name], null);
                    }
                    catch (KeyNotFoundException) { }
                }
            }
        }

    }
}
