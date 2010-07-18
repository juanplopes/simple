using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Simple;
using System.Web.Routing;
using System.Web.Mvc;
using Sample.Project.Web.Controllers;
using System.Reflection;
using Simple.Web.Mvc;

namespace Sample.Project.Web.Helpers
{
    public class DataContextModule : SimpleModule
    {
        protected override void OnBeginRequest(object sender, EventArgs e)
        {
            Simply.Do.EnterContext();
        }

        protected override void OnEndRequest(object sender, EventArgs e)
        {
            Simply.Do.GetContext().Exit();
        }

    }
}
