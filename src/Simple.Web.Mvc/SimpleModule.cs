using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Reflection;
using System.Web.Routing;
using System.Web.Mvc;

namespace Simple.Web.Mvc
{
    public class SimpleModule : IHttpModule
    {
        HttpApplication context;
        protected HttpApplication Context { get { return context; } }
        static bool hasStarted = false;
        static object _lock = new object();

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (!hasStarted)
                lock (_lock)
                {
                    if (!hasStarted)
                    {
                        this.context = context;
                        context.BeginRequest += OnBeginRequest;
                        context.EndRequest += OnEndRequest;
                        context.Error += OnError;
                        hasStarted = true;
                    }
                }
        }

        protected virtual void OnError(object sender, EventArgs e)
        {
            Exception exception = context.Server.GetLastError();
            context.Response.Clear();

            if (exception is HttpException)
                exception = exception.GetBaseException();

            if (exception is TargetInvocationException)
                exception = exception.InnerException;

            if (HandleException(exception))
                context.Server.ClearError();
        }

        protected virtual bool HandleException(Exception exception)
        {
            ExecuteController(CreateRouteData(exception));
            return true;
        }

        protected virtual void ExecuteController(RouteData routeData)
        {
            var httpContext = new HttpContextWrapper(context.Context);
            var requestContext = new RequestContext(httpContext, routeData);
            var factory = ControllerBuilder.Current.GetControllerFactory();
            var controller = factory.CreateController(requestContext, routeData.Values["controller"] as string);
            controller.Execute(requestContext);
        }

        protected virtual RouteData CreateRouteData(Exception exception)
        {
            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "System");
            routeData.Values.Add("action", "Error");
            routeData.Values.Add("exception", exception);

            return routeData;
        }

        protected virtual void OnBeginRequest(object sender, EventArgs e)
        {
        }

        protected virtual void OnEndRequest(object sender, EventArgs e)
        {
        }

    }
}
