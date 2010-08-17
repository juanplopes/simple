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

        public void ShowFinished(bool success, string url)
        {
            if (success)
                SetProgress("Done.", "");
            else
                SetProgress("Done with errors.", "");

            finishedUrl = url;

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
