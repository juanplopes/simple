using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Web.Mvc;
using System.CodeDom.Compiler;

namespace Sample.Project.Web.Controllers
{
    [HandleError]
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index()
        {
            try
            {
                ViewData["Urls"] =
                    this.GetType().Assembly.GetTypes()
                        .Where(x => typeof(IController).IsAssignableFrom(x) && !Attribute.IsDefined(x, typeof(GeneratedCodeAttribute)))
                        .Select(x => x.Name.Remove(x.Name.Length - "Controller".Length)).ToArray();
            }
            catch { ViewData["Urls"] = new string[0]; };

            return View();
        }

        public virtual ActionResult About()
        {
            return View();
        }
    }
}
