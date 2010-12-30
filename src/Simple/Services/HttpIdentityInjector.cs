using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Simple.Services
{
    public class HttpIdentityInjector : BaseCallHook
    {
        public HttpIdentityInjector(CallHookArgs args) : base(args) { }
        public override void Before()
        {
            var http = HttpContext.Current;
            if (http == null) return;

            var user = http.User;
            if (user == null) return;

            var ident = user.Identity;
            if (ident == null) return;

            SimpleContext.Get().Username = ident.IsAuthenticated ?
                ident.Name : null;


        }
    }
}
