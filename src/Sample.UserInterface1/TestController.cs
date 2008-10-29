using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SimpleLibrary.Threading;

namespace Sample.UserInterface1
{
    public class TestController : SimplePageController<TestController>
    {
        public int Test { get; set; }

        public override string InitialPage
        {
            get { return "~/Default.aspx"; }
        }
    }
}
