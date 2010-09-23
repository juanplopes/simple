using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple;
using System.Web.Routing;
using System.Web.Mvc;
using Example.Project.Web.Controllers;
using System.Reflection;
using Simple.Web.Mvc;
using Simple.Data.Context;

namespace Example.Project.Web.Helpers
{
    public class DataContextModule : SimpleModule
    {
        IDataContext context = null;
        protected override void OnBeginRequest(object sender, EventArgs e)
        {
            context = Simply.Do.EnterContext();
        }

        protected override void OnEndRequest(object sender, EventArgs e)
        {
            if (context != null)
                context.Exit();
        }

        protected override void OnError(object sender, EventArgs e)
        {
            //base.OnError(sender, e);
        }

    }
}
