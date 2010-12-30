using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;

namespace Simple.Services
{
    public class WindowsIdentityInjector : BaseCallHook
    {
        public WindowsIdentityInjector(CallHookArgs args) : base(args) { }

        public override void Before()
        {
            var ident = WindowsIdentity.GetCurrent();
            if (ident == null) return;

            SimpleContext.Get().Username = ident.IsAuthenticated ?
                ident.Name : null;
        }
    }
}
