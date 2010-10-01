using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Example.Project.Config;
using Simple;
using Example.Project.Web.Controllers;
using Example.Project.Web.Helpers;
using Simple.Entities;
using Simple.Reflection;
using Simple.Web.Mvc;
using Simple.Threading;

namespace Example.Project.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            MapDefault(routes, "Default", 
                "{controller}/{action}/{id}",
                "{controller}.{format}",
                "{controller}/{action}.{format}",
                "{controller}/{action}/{id}.{format}"
            );
        }

        protected static void MapDefault(RouteCollection routes, string name, params string[] patterns)
        {
            for (int i = 0; i < patterns.Length; i++)
            {
                routes.MapRoute(name + i, patterns[i],
                    new { controller = "Home", action = "Index", id = 0, format = "html" },
                    new { controller = "[^\\.]*", action = "[^\\.]*", id = "[^\\.]*", format = "[^\\.]*" }
                );
            }
        }

        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            DefaultModelBinder.ResourceClassKey = "ValidationMessages";
            ModelBinders.Binders.DefaultBinder = new EntityModelBinder();

            ModelValidatorProviders.Providers.Clear();

            SimpleContext.SwitchProvider(new HttpContextProvider());
            new Configurator().StartServer<ServerStarter>();
        }

        protected void Application_BeginRequest()
        {
            Simply.Do.EnterContext();
        }
        protected void Application_EndRequest()
        {
            var ctx = Simply.Do.GetContext(false);
            if (ctx != null)
                ctx.Dispose();
        }
    }
}