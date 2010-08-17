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
using System.Diagnostics;
using System.Threading;

namespace Simple.Gui
{
    public partial class ProgressGui : Form
    {
        string finishedUrl = null;
        public ProgressGui()
        {
            InitializeComponent();
        }

        public void SetProgress(string text, int value)
        {
            this.ProgressText.Text = text;
            if (value < 0 || value > 100)
                this.progressBar1.Style = ProgressBarStyle.Marquee;
            else
            {
                this.progressBar1.Style = ProgressBarStyle.Continuous;
                this.progressBar1.Value = value;
            }
        }

        public void ShowFinished(bool success, string url)
        {
            if (success)
                SetProgress("Done.", 100);
            else
                SetProgress("Done with errors.", 100);

            finishedUrl = url;

            progressBar1.Visible = false;

            if (url != null) btnStart.Show();
            this.btnClose.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (finishedUrl != null)
                Process.Start(finishedUrl);

            this.Close();
        }
    }
}
