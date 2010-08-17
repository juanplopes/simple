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

        public void SetProgress(string text, string subText)
        {
            if (text != null)
                this.ProgressText.Text = text;

            if (subText != null)
                this.ProgressSubText.Text = subText;

        }

        public void ShowFinished(string success, string url)
        {
            SetProgress(success, "");
            finishedUrl = url;

            btnClose.Show();
            btnStart.Show();

            btnClose.Enabled = true;
            if (url != null)
                btnStart.Enabled = true;

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

        private void ProgressText_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, ProgressText.Text, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
