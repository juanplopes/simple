using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Sample.Project.UserInterface
{
    public partial class _Default : Page
    {
        public void Page_Load(object sender, System.EventArgs e)
        {
            Response.Redirect("~/Home");
        }
    }
}
