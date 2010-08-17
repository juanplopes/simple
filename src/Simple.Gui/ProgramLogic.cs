using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;
using System.Net;
using System.Windows.Forms;

namespace Simple.Gui
{
    public class ProgramLogic
    {
        public string Namespace { get; set; }
        public string Catalog { get; set; }
        public string IISUrl { get; set; }
        public string ServiceName { get; set; }
        public string ReplacePath { get; set; }
        public string InstallPath { get; set; }
        public bool SetupEnv { get; set; }

        public event Action<string, string> OnProgress;
        protected void ReportProgress(string text, string subText)
        {
            if (OnProgress != null)
                OnProgress(text, subText);
        }

        public event Action<string, string> OnFinish;
        protected void ReportFinish(string success, string url)
        {
            if (OnFinish != null)
                OnFinish(success, url);
        }

        public void Execute()
        {
            try
            {
                DoReplace();

                ReportProgress("Copying files", "<starting>");
                CopyDirectory(null, ReplacePath, InstallPath);


                if (SetupEnv)
                {
                    ReportProgress("Setting up development environment", "Ensuring .Net framework is on path");
                    EnsureNetFxPath();

                    int msbuild = RunMsBuild();

                    if (msbuild == 0)
                    {
                        var url = string.Format("http://localhost/{0}", IISUrl);

                        PrecacheResults(url);
                        ReportFinish("Done", url);
                    }
                    else
                        ReportFinish("Done, with errors", null);
                }
                else
                {
                    ReportFinish("Done", null);
                }
            }
            catch(Exception e)
            {
                ReportFinish(string.Format("Fatal error: {0}", e.Message), null);
            }
        }

        private void DoReplace()
        {
            ReportProgress("Replacing default template", "Step 1 of 4");
            ReplacerLogic.DefaultExecute(ReplacePath, "Example.Project", Namespace, true);
            ReportProgress(null, "Step 2 of 4");
            ReplacerLogic.DefaultExecute(ReplacePath, "ExampleProject", Catalog, false);
            ReportProgress(null, "Step 3 of 4");
            ReplacerLogic.DefaultExecute(ReplacePath, "example-project", IISUrl, false);
            ReportProgress(null, "Step 4 of 4");
            ReplacerLogic.DefaultExecute(ReplacePath, "exampleprojectsvc", ServiceName, false);
        }

        private void PrecacheResults(string url)
        {
            int retries = 0;
            while (retries++ < 3)
            {
                ReportProgress(string.Format("Precaching results ({0}/3)", retries),
                    string.Format("Accessing '{0}'", url));
                try
                {
                    WebRequest.Create(url).GetResponse().GetResponseStream().ReadByte();
                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
        }

        private void EnsureNetFxPath()
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

        private string GetNetFxPath()
        {
            var dotnetPath = ToolLocationHelper.GetPathToDotNetFramework(
                TargetDotNetFrameworkVersion.VersionLatest);
            return dotnetPath;
        }

        private void CopyDirectory(string baseDir, string src, string dst)
        {
            String[] Files;
            baseDir = baseDir ?? src;

            if (dst[dst.Length - 1] != Path.DirectorySeparatorChar)
                dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(dst)) Directory.CreateDirectory(dst);
            Files = Directory.GetFileSystemEntries(src);

            foreach (string Element in Files)
            {
                ReportProgress(null, Element.Substring(baseDir.Length + 1));

                if (Directory.Exists(Element))
                    CopyDirectory(baseDir, Element, dst + Path.GetFileName(Element));
                else
                    File.Copy(Element, dst + Path.GetFileName(Element), true);
            }
        }

        public static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
        }


        public int RunMsBuild()
        {
            try
            {
                ReportProgress("Building project", "Running MSBuild");

                var psi = new ProcessStartInfo();
                psi.FileName = "msbuild";
                psi.Arguments = string.Format("first-build.xml \"/p:WebVirtualPath={0}\"", IISUrl);
                psi.WorkingDirectory = InstallPath;
                psi.WindowStyle = ProcessWindowStyle.Hidden;

                if (!IsAdmin())
                    psi.Verb = "runas";

                var p = Process.Start(psi);
                p.WaitForExit();
                if (p.ExitCode == 0)
                    File.Delete(Path.Combine(InstallPath, "first-build.xml"));

                return p.ExitCode;
            }
            catch
            {
                return 1;
            }
        }
    }
}
