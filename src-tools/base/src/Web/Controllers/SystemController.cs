using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Simple.Patterns;
using Simple;
using Example.Project.Services;
using System.Reflection;
using Example.Project.Web.Helpers;
using Simple.Web.Mvc;

namespace Example.Project.Web.Controllers
{
    public partial class SystemController : Controller
    {
        public SystemController()
        {
            TempDataProvider = new NullTempDataProvider();
        }

        //
        // GET: /System/
        public virtual ActionResult Index()
        {
            var runner = new TaskRunner();

            runner.RunChained("Can access business server", "Success",
                x => Simply.Do.Resolve<ISystemService>().Check());

            return View(runner.Results);
        }

        public virtual ActionResult Error(Exception exception)
        {
            ViewData["code"] = exception.GetType().Name;
            ViewData["message"] = exception.Message;

            ViewData["stacktrace"] = "";
            while (exception != null)
            {
                ViewData["stacktrace"] += exception.GetType().Name + "\n"+ exception.StackTrace+ "\n\n";
                exception = exception.InnerException;
            }
            Response.StatusCode = 500;
            return View();
        }

        protected string ErrorMessage(int id)
        {
            switch (id)
            {
                case 400: return "Bad request";
                case 401: return "Unauthorized access";
                case 402: return "Access forbidden";
                case 403: return "Forbidden";
                case 404: return "Resource not found";
                case 405: return "Method not Allowed";
                case 408: return "Request timeout";
                case 500: return "Internal Server Error";
                default: return "Error";
            }

        }

    }
}
