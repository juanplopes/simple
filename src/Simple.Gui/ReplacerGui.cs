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
using Microsoft.Build.Utilities;

namespace Simple.Gui
{
    public partial class ReplacerGui : Form
    {
        public ReplacerGui()
        {
            InitializeComponent();
            txtNamespace_TextChanged(this, null);

            AdvancedGroup.Visible = false;
            AutoResize();
        }

        public static void InvokeControlAction<T>(T cont, Action<T> action) where T : Control
        {
            if (cont.InvokeRequired)
            {
                cont.Invoke(new Action<T, Action<T>>(InvokeControlAction),
                          new object[] { cont, action });
            }
            else
            { action(cont); }
        }

        private void AutoResize()
        {
            if (AdvancedGroup.Visible)
            {
                this.Height = AdvancedGroup.Top + AdvancedGroup.ClientSize.Height + 2 * AdvancedGroup.Left + btnOk.Height;
                txtCatalog.Focus();
                btnMore.Text = "<< Less";
            }
            else
            {
                this.Height = AdvancedGroup.Top + AdvancedGroup.Left + btnOk.Height;
                txtNamespace.Focus();
                btnMore.Text = "More >>";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");
            string message = "Temporary data is on:\n" + path + "\n\nAnd will be installed on:\n" + btnDirectory.Text + "\n\nAre you sure?";

            if (MessageBox.Show(message, "Simple.Net", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();

                var progress = new ProgressGui();
                progress.SetText("Creating project...");

                var t = new Thread(() =>
                {

                    InvokeControlAction(progress, x => x.SetText("Replacing default template..."));

                    ReplacerLogic.DefaultExecute(path, "Example.Project", txtNamespace.Text.Trim(), true);
                    ReplacerLogic.DefaultExecute(path, "ExampleProject", txtCatalog.Text.Trim(), false);
                    ReplacerLogic.DefaultExecute(path, "example-project", txtIISUrl.Text.Trim(), false);
                    ReplacerLogic.DefaultExecute(path, "exampleprojectsvc", txtSvcName.Text.Trim(), false);

                    InvokeControlAction(progress, x => x.SetText("Copying directory..."));

                    CopyDirectory(path, btnDirectory.Text);

                    if (chkPrepare.Checked)
                    {
                        InvokeControlAction(progress, x => x.SetText("Preparing environment..."));

                        EnsureNetFxPath();

                        InvokeControlAction(progress, x => x.SetText("Building project for the first time..."));

                        if (RunMsBuild() == 0)
                        {
                            InvokeControlAction(progress, x => x.SetText("Done."));
                            InvokeControlAction(progress, x => x.ShowFinished(string.Format("http://localhost/{0}", txtIISUrl.Text)));
                        }
                        else
                        {
                            InvokeControlAction(progress, x => x.SetText("Done with errors."));
                            InvokeControlAction(progress, x => x.ShowFinished(null));
                        }
                    }
                    else
                    {
                        InvokeControlAction(progress, x => x.ShowFinished(null));
                    }
                });
                t.Start();

                progress.ShowDialog(this);
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

            var instDir = txtIISUrl.Text;

            if (e == null)
                btnDirectory.Text = Path.Combine(Environment.CurrentDirectory, instDir);
            else
                btnDirectory.Text = Path.Combine(Path.GetDirectoryName(btnDirectory.Text) ?? btnDirectory.Text, instDir);
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            AdvancedGroup.Visible = !AdvancedGroup.Visible;
            AutoResize();
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.livingnet.com.br");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = btnDirectory.Text;
            folderBrowser.ShowDialog(this);
            btnDirectory.Text = folderBrowser.SelectedPath;
        }

        public int RunMsBuild()
        {
            try
            {
                var psi = new ProcessStartInfo();
                psi.FileName = "msbuild";
                psi.Arguments = string.Format("first-build.xml \"/p:WebVirtualPath={0}\"", txtIISUrl.Text);
                psi.WorkingDirectory = btnDirectory.Text;
                psi.Verb = "runas";
                psi.WindowStyle = ProcessWindowStyle.Hidden;

                var p = Process.Start(psi);
                p.WaitForExit();
                return p.ExitCode;
            }
            catch
            {
                return 1;
            }
        }

        public void EnsureNetFxPath()
        {
            var dotnetPath = GetNetFxPath();

            var paths = new Paths(Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.Machine));
            var userPaths = new Paths(Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User));

            if (!paths.Contains(dotnetPath) && !userPaths.Contains(dotnetPath))
            {
                userPaths.Add(dotnetPath);
                Environment.SetEnvironmentVariable("PATH",
                    userPaths.ToString(), EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable("PATH",
                    userPaths.ToString(), EnvironmentVariableTarget.Process);
            }
        }

        private static string GetNetFxPath()
        {
            var dotnetPath = ToolLocationHelper.GetPathToDotNetFramework(
                TargetDotNetFrameworkVersion.VersionLatest);
            return dotnetPath;
        }

        public static void CopyDirectory(string src, string dst)
        {
            String[] Files;

            if (dst[dst.Length - 1] != Path.DirectorySeparatorChar)
                dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(dst)) Directory.CreateDirectory(dst);
            Files = Directory.GetFileSystemEntries(src);

            foreach (string Element in Files)
            {
                if (Directory.Exists(Element))
                    CopyDirectory(Element, dst + Path.GetFileName(Element));
                else
                    File.Copy(Element, dst + Path.GetFileName(Element), true);
            }
        }


    }
}

