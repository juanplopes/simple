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
            Version.Text = string.Format("v{0} (build {1})", this.GetType().Assembly.GetName().Version.ToString(3), VersionName.Text);

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
            string message = "Project will be installed at: " + path + "\n\nAre you sure?";
            if (MessageBox.Show(message, "Simple.Net", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ReplacerLogic.DefaultExecute(path, "Example.Project", txtNamespace.Text.Trim(), true);
                ReplacerLogic.DefaultExecute(path, "ExampleProject", txtCatalog.Text.Trim(), false);
                ReplacerLogic.DefaultExecute(path, "example-project", txtIISUrl.Text.Trim(), false);
                ReplacerLogic.DefaultExecute(path, "exampleprojectsvc", txtSvcName.Text.Trim(), false);

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

        private Point ClickedPoint;
        private bool IsDragging = false;

        private void Drag_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            this.IsDragging = true;
            this.ClickedPoint = new Point(e.X, e.Y);

        }

        private void Drag_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            this.IsDragging = false;

        }

        private void Drag_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (this.IsDragging)
            {
                Point NewPoint;

                NewPoint = this.PointToScreen(new Point(e.X, e.Y));
                NewPoint.Offset(this.ClickedPoint.X * -1, this.ClickedPoint.Y * -1);

                this.Location = NewPoint;

            }
        }

    }
}
