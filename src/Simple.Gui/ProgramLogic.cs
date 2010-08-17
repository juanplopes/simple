using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Security.Principal;

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
        public bool PrepareEnv { get; set; }

        public event Action<string, int> OnProgress;
        protected void ReportProgress(string text, int value)
        {
            if (OnProgress != null)
                OnProgress(text, value);
        }

        public event Action<bool, string> OnFinish;
        protected void ReportFinish(bool success, string url)
        {
            if (OnFinish != null)
                OnFinish(success, url);
        }


        private int _progress = 10;
        protected int Represents(int value)
        {
            var old = _progress;
            _progress += value;
            return old;
        }

        public void Execute()
        {
            ReportProgress("Replacing default template...", Represents(20));

            ReplacerLogic.DefaultExecute(ReplacePath, "Example.Project", Namespace, true);
            ReplacerLogic.DefaultExecute(ReplacePath, "ExampleProject", Catalog, false);
            ReplacerLogic.DefaultExecute(ReplacePath, "example-project", IISUrl, false);
            ReplacerLogic.DefaultExecute(ReplacePath, "exampleprojectsvc", ServiceName, false);

            ReportProgress("Copying directory...", Represents(30));

            CopyDirectory(ReplacePath, InstallPath);


            if (PrepareEnv)
            {
                ReportProgress("Preparing environment...", Represents(20));
                EnsureNetFxPath();


                ReportProgress("Building project...", -1);
                if (RunMsBuild() == 0)
                    ReportFinish(true, string.Format("http://localhost/{0}", IISUrl));
                else
                    ReportFinish(false, null);
            }
            else
            {
                ReportFinish(true, null);
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
