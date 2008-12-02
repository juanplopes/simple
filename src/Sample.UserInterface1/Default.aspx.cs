using System;
using SimpleLibrary.Threading;
using SimpleLibrary.DataAccess;
using Sample.BusinessInterface.Domain;
using Sample.BusinessInterface;
using SimpleLibrary.Rules;
using System.Web.UI.WebControls;

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
        //TestController controller = null;

        protected override void OnInit(EventArgs e)
        {
          //  controller = TestController.Get(1, this);
           // controller.Ensure();
            base.OnInit(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            IEmpresaFuncionarioRules rules = RulesFactory.Create<IEmpresaFuncionarioRules>();
            var src = SimpleDataSource.Create(rules);
            DetailsView1.DataSource = src;
            DetailsView1.DataBind();
        }
    }
}
