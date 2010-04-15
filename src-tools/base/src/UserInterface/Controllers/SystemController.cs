using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Simple.Patterns;
using Simple;
using Sample.Project.Services;

namespace Sample.Project.Web.Controllers
{
    public class SystemController : Controller
    {
        //
        // GET: /System/
        public ActionResult Check()
        {
            var runner = new TaskRunner();

            runner.RunChained("Can access business server", "Success",
                x => Simply.Do.Resolve<ISystemService>().Check());

            return View(runner.Results);
        }

    }
}
