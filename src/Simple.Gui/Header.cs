using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Simple.Gui
{
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
            Version.Text = string.Format("v{0} (build {1})", this.GetType().Assembly.GetName().Version.ToString(3), VersionName.Text);
            RunningAs.Text = ProgramLogic.IsAdmin() ? "Administrator" : "Standard user";
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

                NewPoint = ParentForm.PointToScreen(new Point(e.X, e.Y));
                NewPoint.Offset(this.ClickedPoint.X * -1, this.ClickedPoint.Y * -1);

                ParentForm.Location = NewPoint;

            }
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            if (ParentForm.Controls.Find("txtNamespace", true).First().Text.ToLower() == "dirtyhack")
                new SimpleOtherGui().Show();
        }


    }
}
