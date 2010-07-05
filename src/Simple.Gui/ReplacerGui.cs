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

namespace Simple.Gui
{
    public partial class ReplacerGui : Form
    {
        public ReplacerGui()
        {
            InitializeComponent();
            txtNamespace_TextChanged(this, new EventArgs());
            Version.Text = string.Format("v{0}", this.GetType().Assembly.GetName().Version.ToString(3));

            AdvancedGroup.Visible = false;
            AutoResize();
        }

        private void AutoResize()
        {
            if (AdvancedGroup.Visible)
            {
                this.Height = AdvancedGroup.Top + AdvancedGroup.ClientSize.Height + AdvancedGroup.Left;
                txtCatalog.Focus();
            }
            else
            {
                this.Height = AdvancedGroup.Top + AdvancedGroup.Left;
                txtNamespace.Focus();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string message = "Replacer will be executed at: " + path + Environment.NewLine + "Are you sure?";
            if (MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ReplacerLogic.DefaultExecute(path, "Sample.Project", txtNamespace.Text.Trim(), true);
                ReplacerLogic.DefaultExecute(path, "SampleProject", txtCatalog.Text.Trim(), false);
                ReplacerLogic.DefaultExecute(path, "sample-project", txtIISUrl.Text.Trim(), false);
                ReplacerLogic.DefaultExecute(path, "sampleprojectsvc", txtSvcName.Text.Trim(), false);

                MessageBox.Show("Done!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtNamespace_TextChanged(object sender, EventArgs e)
        {
            txtCatalog.Text = txtNamespace.Text.Replace(".", "");
            txtIISUrl.Text = txtNamespace.Text.Replace(".", "-").ToLower();
            txtSvcName.Text = txtNamespace.Text.Replace(".", "").ToLower() + "svc";
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (txtNamespace.Text.ToLower() == "dirtyhack")
                new SimpleOtherGui().Show();
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            AdvancedGroup.Visible = !AdvancedGroup.Visible;
            AutoResize();
        }

       
    }
}
