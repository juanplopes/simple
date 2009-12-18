using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace Simple.GUI
{
    public partial class SimpleOtherGui : Form
    {
        public SimpleOtherGui()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string message = "Replacer will be executed at: " + path + Environment.NewLine + "Are you sure?";
            if (MessageBox.Show(message, "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ReplacerLogic.DefaultExecute(path, textBox1.Text, textBox2.Text);
                MessageBox.Show("Done!", "Message");
                this.Close();
            }
        }
    }
}
