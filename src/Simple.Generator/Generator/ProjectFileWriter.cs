using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using log4net;
using System.Reflection;

namespace Simple.Generator
{
    public class ProjectFileWriter : ProjectWriter, IDisposable
    {
        public string ProjectPath { get; protected set; }
        ILog log = Simply.Do.Log(MethodInfo.GetCurrentMethod());
        bool autocommit = false;
        string lastXml = null;

        public ProjectFileWriter(string projectPath) :
            base(File.ReadAllText(projectPath))
        {
            log.DebugFormat("Read project '{0}'", projectPath);
            ProjectPath = Path.GetFullPath(projectPath);
            lastXml = GetXml();
        }

        public ProjectFileWriter AutoCommit()
        {
            autocommit = true;
            return this;
        }

        public ProjectFileWriter ManualCommit()
        {
            autocommit = false;
            return this;
        }

        public ProjectFileWriter AddNewNone(string relativePath, string content)
        {
            return AddNewFile(relativePath, None, content);
        }


        public ProjectFileWriter AddNewNone(string relativePath, byte[] content)
        {
            return AddNewFile(relativePath, None, content);
        }


        public ProjectFileWriter AddNewEmbeddedResource(string relativePath, string content)
        {
            return AddNewFile(relativePath, EmbeddedResource, content);
        }


        public ProjectFileWriter AddNewEmbeddedResource(string relativePath, byte[] content)
        {
            return AddNewFile(relativePath, EmbeddedResource, content);
        }

        public ProjectFileWriter AddNewCompile(string relativePath, string content)
        {
            return AddNewFile(relativePath, Compile, content);
        }

        public ProjectFileWriter AddNewCompile(string relativePath, byte[] content)
        {
            return AddNewFile(relativePath, Compile, content);
        }

        public ProjectFileWriter AddNewFile(string relativePath, string type, string content)
        {
            var info = CreateFile(relativePath, content);
            AddFile(relativePath, type);
            return info;
        }

        public ProjectFileWriter AddNewFile(string relativePath, string type, byte[] content)
        {
            var info = CreateFile(relativePath, content);
            AddFile(relativePath, type);
            return info;
        }

        public ProjectFileWriter CreateFile(string relativePath, string content)
        {
            return CreateFile(relativePath, content, File.WriteAllText);
        }

        public ProjectFileWriter CreateFile(string relativePath, byte[] content)
        {
            return CreateFile(relativePath, content, File.WriteAllBytes);
        }

        public ProjectFileWriter RemoveAndDeleteFile(string relativePath)
        {

            var path = GetFullPath(relativePath);

            if (File.Exists(path))
            {
                log.DebugFormat("Deleting file '{0}'...", relativePath);
                File.Delete(path);
            }
            RemoveFile(relativePath);
            return this;
        }

        public bool ExistsFile(string relativePath)
        {
            return File.Exists(GetFullPath(relativePath));
        }

        protected ProjectFileWriter CreateFile<T>(string relativePath, T content, Action<string, T> writer)
        {
            log.DebugFormat("Creating file '{0}'...", relativePath);

            var fullDir = GetFullPath(relativePath);
            var dir = Path.GetDirectoryName(fullDir);
            Directory.CreateDirectory(dir);
            writer(fullDir, content);
            return this;
        }

        public string GetFullPath(string relativePath)
        {
            var fullDir = Path.Combine(Path.GetDirectoryName(ProjectPath), relativePath);
            fullDir = CorrectPaths(fullDir);
            return fullDir;
        }

        public ProjectFileWriter WriteChanges()
        {
            var xml = GetXml();

            if (xml != lastXml)
            {
                log.DebugFormat("Writing changes to project '{0}'...", Path.GetFileName(ProjectPath));
                File.WriteAllText(ProjectPath, xml);
                lastXml = xml;
            }
            else
            {
                log.DebugFormat("No change was made to '{0}'. Skipping...", Path.GetFileName(ProjectPath));
            }

            return this;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (autocommit)
                WriteChanges();
        }

        #endregion
    }
}
