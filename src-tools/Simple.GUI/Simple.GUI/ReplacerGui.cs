using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace Simple.GUI
{
    public partial class ReplacerGui : Form
    {
        public ReplacerGui()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string message = "Replacer will be executed at: " + path + Environment.NewLine + "Are you sure?";
            if (MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ReplacerLogic.DefaultExecute(path, "Sample.Project", txtNamespace.Text.Trim());
                ReplacerLogic.DefaultExecute(path, "SampleProject", txtCatalog.Text.Trim());
                ReplacerLogic.DefaultExecute(path, "sample-project", txtIISUrl.Text.Trim());
                ReplacerLogic.DefaultExecute(path, "sampleprojectsvc", txtSvcName.Text.Trim());
                ReplacerLogic.DefaultExecute(path, "sampleprojectSchemaInfo", txtSchemaInfo.Text.Trim());

                MessageBox.Show("Done!", "Message");
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            if (btnMore.Tag == null)
            {
                btnMore.Text = "Less";
                this.Height = 475;
                btnMore.Tag = new object();
            }
            else
            {
                btnMore.Text = "More";
                this.Height = 316;
                btnMore.Tag = null;
            }
        }

        private void txtNamespace_TextChanged(object sender, EventArgs e)
        {
            txtCatalog.Text = txtNamespace.Text.Replace(".", "");
            txtIISUrl.Text = txtNamespace.Text.Replace(".", "-").ToLower();
            txtSvcName.Text = txtNamespace.Text.Replace(".", "").ToLower() + "svc";
            txtSchemaInfo.Text = txtNamespace.Text.Replace(".", "") + "SchemaInfo";
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (txtNamespace.Text.ToLower() == "dirtyhack")
                new SimpleOtherGui().Show();
        }

       
    }
}
