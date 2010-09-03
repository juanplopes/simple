using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simple.Site.Controllers
{
    public class DownloadController : Controller
    {
        //
        // GET: /Download/

        public ActionResult Latest()
        {
            return Redirect("http://simpledotnet.googlecode.com/files/Simple.Avalon.exe");
        }

    }
}
