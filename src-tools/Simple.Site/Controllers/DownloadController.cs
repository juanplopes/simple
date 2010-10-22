using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple.Site.Controllers
{
    public class DownloadController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Latest()
        {
            return Redirect("http://teamcity.codebetter.com/repository/download/bt219/.lastSuccessful/simple-3.1-scaffold.exe");
        }

    }
}
