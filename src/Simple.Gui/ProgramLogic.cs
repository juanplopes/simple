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
using System.Security.AccessControl;
using System.Reflection;

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

        public event Action<string, string> OnProgress;
        public Func<string, bool> OnAsk { get; set; }

        protected void ReportProgress(string text, string subText)
        {
            if (OnProgress != null)
                OnProgress(text, subText);
        }

        protected bool Ask(string text)
        {
            if (OnAsk != null)
                return OnAsk(text);

            return true;
        }


        public event Action<string> OnFinish;
        protected void ReportFinish(string success)
        {
            if (OnFinish != null)
                OnFinish(success);
        }

        public void Execute()
        {
            try
            {
                DoReplace();

                ReportProgress("Copying files", "<starting>");
                CopyItem(null, ReplacePath, InstallPath);
                SetPermissions(InstallPath);

                ReportFinish("Done!");
             
            }
            catch (Exception e)
            {
                ReportFinish(string.Format("Fatal error: {0}", e.Message));
            }
        }

        private void SetPermissions(string file)
        {
            var sec = Directory.GetAccessControl(file);
            sec.AddAccessRule(
                 new FileSystemAccessRule(
                     new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null),
                     FileSystemRights.FullControl,
                     InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit,
                     PropagationFlags.InheritOnly,
                     AccessControlType.Allow));
            Directory.SetAccessControl(file, sec);
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

     
        private void CopyItem(string baseDir, string source, string destination)
        {
            baseDir = baseDir ?? source;

            ReportProgress(null, source.Substring(baseDir.Length - 1));

            if (Directory.Exists(source))
            {
                if (!Directory.Exists(destination))
                    Directory.CreateDirectory(destination);

                foreach (var item in Directory.GetFileSystemEntries(source))
                    CopyItem(baseDir, item, Path.Combine(destination, Path.GetFileName(item)));
            }
            else
            {
                File.Copy(source, destination, true);
            }


        }

        public static bool IsAdmin()
        {
            return new WindowsPrincipal(WindowsIdentity.GetCurrent())
                .IsInRole(WindowsBuiltInRole.Administrator);
        }

    }
}
