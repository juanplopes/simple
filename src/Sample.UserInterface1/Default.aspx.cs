using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Portal
{
    public partial class Default : System.Web.UI.Page
    {
        public class Pessoa
        {
            public string Nome { get; set; }
            public string CPF { get; set; }
            public string Sobrenome { get; set; }
            public DateTime DtNascimento { get; set; }
            public string Telefone1 { get; set; }
            public string Telefone2 { get; set; }
            public string Sexo { get; set; }
            public string Endereco1 { get; set; }
            public string Endereco2 { get; set; }
            public string Estado { get; set; }
            public string Cidade { get; set; }
            public string Pais { get; set; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack) return;

            var lobjPessoa = new Pessoa();
            lobjPessoa.Nome = "Lincoln";
            lobjPessoa.CPF = "09284166748";
            lobjPessoa.Sobrenome = "Quinan Junior";
            lobjPessoa.DtNascimento = Convert.ToDateTime("01/10/1981");
            lobjPessoa.Telefone1 = "(24)2245-0375";
            lobjPessoa.Telefone2 = "(24)9961-1330";
            lobjPessoa.Sexo = "M";
            lobjPessoa.Endereco1 = "Av Tiradentes";
            lobjPessoa.Endereco2 = "61/308";
            lobjPessoa.Estado = "RJ";
            lobjPessoa.Cidade = "Petrópolis";
            lobjPessoa.Pais = "Brasil";

            var llisPessoas = new List<Pessoa>();
            llisPessoas.Add(lobjPessoa);
            frmCadastro.DataSource = llisPessoas;
            frmCadastro.DataBind();

            RadioButtonList rblSexo = (RadioButtonList)frmCadastro.FindControl("rblSexo");
            if (lobjPessoa.Sexo == "M")
                rblSexo.Items[0].Selected = true;
            else
                rblSexo.Items[1].Selected = true;

            DropDownList cboEstado = (DropDownList)frmCadastro.FindControl("cboEstado");
            cboEstado.Items.FindByValue(lobjPessoa.Estado).Selected = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

    }
}
