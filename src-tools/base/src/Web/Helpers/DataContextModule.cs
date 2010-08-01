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
        protected override void OnBeginRequest(object sender, EventArgs e)
        {
            Context.Context.Items["DataContext"] = Simply.Do.EnterContext();
        }

        protected override void OnEndRequest(object sender, EventArgs e)
        {
            var context = Context.Context.Items["DataContext"] as DataContext;
            if (context != null)
                context.Exit();
        }

    }
}
