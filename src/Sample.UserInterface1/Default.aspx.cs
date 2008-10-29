using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Sample.BusinessInterface;
using SimpleLibrary.Rules;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface.Domain;

namespace Sample.UserInterface1
{
    public partial class _Default : System.Web.UI.Page
    {
        TestController controller = null;

        protected override void OnInit(EventArgs e)
        {
            controller = TestController.Get(1, this);
            controller.Ensure();
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataSource = new EntityDataSource<Empresa, IEmpresaRules>();
            GridView1.DataBind();
        }
    }
}
