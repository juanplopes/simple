using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Principal;
using System.Runtime.Remoting.Messaging;

namespace Simple.Services
{
    public class HttpIdentityInjector : BaseCallHook
    {
        public HttpIdentityInjector(CallHookArgs args) : base(args) { }
        public override void Before()
        {
            try
            {
                var ident = HttpContext.Current.User.Identity;

                SimpleContext.Get().Username = ident.IsAuthenticated ?
                    ident.Name : null;
            }
            catch (NullReferenceException)
            {
                Simply.Do.Log("NullReference skipped");
            }


        }
    }

    public class WindowsIdentityInjector : BaseCallHook
    {
        public WindowsIdentityInjector(CallHookArgs args) : base(args) { }
        public override void Before()
        {
            try
            {
                var ident = WindowsIdentity.GetCurrent();
                SimpleContext.Get().Username = ident.IsAuthenticated ?
                    ident.Name : null;
            }
            catch (NullReferenceException)
            {
                Simply.Do.Log("NullReference skipped");
            }
        }
    }

    [Serializable]
    public class SimpleContext
    {
        public string Username { get; set; }
        public object ConfigKey { get; set; }

        public static SimpleContext Get()
        {
            SimpleContext context = CallHeaders.Do[typeof(SimpleContext).GUID.ToString()] as SimpleContext;
            if (context == null)
            {
                CallHeaders.Do[typeof(SimpleContext).GUID.ToString()] = context = new SimpleContext();
            }
            return context;
        }

    }
}
