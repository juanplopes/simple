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

        public event Action<string> OnProgress;
        protected void ReportProgress(string text)
        {
            if (OnProgress != null)
                OnProgress(text);
        }

        public event Action<bool, string> OnFinish;
        protected void ReportFinish(bool success, string url)
        {
            if (OnFinish != null)
                OnFinish(success, url);
        }

        public void Execute()
        {
            ReportProgress("Replacing default template...");

            ReplacerLogic.DefaultExecute(ReplacePath, "Example.Project", Namespace, true);
            ReplacerLogic.DefaultExecute(ReplacePath, "ExampleProject", Catalog, false);
            ReplacerLogic.DefaultExecute(ReplacePath, "example-project", IISUrl, false);
            ReplacerLogic.DefaultExecute(ReplacePath, "exampleprojectsvc", ServiceName, false);

            ReportProgress("Copying directory...");

            CopyDirectory(ReplacePath, InstallPath);


            if (SetupEnv)
            {
                ReportProgress("Setting up development environment...");
                EnsureNetFxPath();


                ReportProgress("Building project...");

                int msbuild = RunMsBuild();

                if (msbuild == 0)
                {
                    var url = string.Format("http://localhost/{0}", IISUrl);

                    PrecacheResults(url);
                    ReportFinish(true, url);
                }
                else
                    ReportFinish(false, null);
            }
            else
            {
                ReportFinish(true, null);
            }
        }

        private void PrecacheResults(string url)
        {
            int retries = 0;
            while (retries++ < 3)
            {
                ReportProgress(string.Format("Precaching results ({0}/3)...", retries));
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

        private static string GetNetFxPath()
        {
            var dotnetPath = ToolLocationHelper.GetPathToDotNetFramework(
                TargetDotNetFrameworkVersion.VersionLatest);
            return dotnetPath;
        }

        private static void CopyDirectory(string src, string dst)
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

        public static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
        }


        public int RunMsBuild()
        {
            try
            {
                var psi = new ProcessStartInfo();
                psi.FileName = "msbuild";
                psi.Arguments = string.Format("first-build.xml \"/p:WebVirtualPath={0}\"", IISUrl);
                psi.WorkingDirectory = InstallPath;

                if (!IsAdmin())
                    psi.Verb = "runas";

                psi.WindowStyle = ProcessWindowStyle.Hidden;

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
