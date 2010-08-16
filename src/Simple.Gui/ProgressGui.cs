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

namespace Simple.Gui
{
    public partial class ProgressGui : Form
    {
        string finishedUrl = null;
        public ProgressGui()
        {
            InitializeComponent();
        }

        public void SetText(string text)
        {
            this.ProgressText.Text = text;
        }

        public void ShowFinished(string url)
        {
            finishedUrl = url;

            if (url != null) btnStart.Show();
            this.btnClose.Show();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

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
