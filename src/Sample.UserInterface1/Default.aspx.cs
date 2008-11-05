using System;
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

    public partial class _Default : System.Web.UI.Page
    {
        TestController controller = null;

        protected override void OnInit(EventArgs e)
        {
            controller = TestController.Get(1, this);
            controller.Ensure();
            base.OnInit(e);
        }
    }
}
