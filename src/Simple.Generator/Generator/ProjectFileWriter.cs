using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.Generator
{
    public class ProjectFileWriter : ProjectWriter
    {
        public string ProjectPath { get; protected set; }

        public ProjectFileWriter(string projectPath) :
            base(File.ReadAllText(projectPath))
        {
            ProjectPath = Path.GetFullPath(projectPath);
        }

        public FileInfo AddNewEmbeddedResource(string relativePath, string content)
        {
            return AddNewFile(relativePath, EmbeddedResource, content);
        }


        public FileInfo AddNewEmbeddedResource(string relativePath, byte[] content)
        {
            return AddNewFile(relativePath, EmbeddedResource, content);
        }

        public FileInfo AddNewCompile(string relativePath, string content)
        {
            return AddNewFile(relativePath, Compile, content);
        }

        public FileInfo AddNewCompile(string relativePath, byte[] content)
        {
            return AddNewFile(relativePath, Compile, content);
        }

        public FileInfo AddNewFile(string relativePath, string type, string content)
        {
            var info = CreateFile(relativePath, content);
            AddFile(relativePath, type);
            return info;
        }

        public FileInfo AddNewFile(string relativePath, string type, byte[] content)
        {
            var info = CreateFile(relativePath, content);
            AddFile(relativePath, type);
            return info;
        }

        public FileInfo CreateFile(string relativePath, string content)
        {
            return CreateFile(relativePath, content, File.WriteAllText);
        }

        public FileInfo CreateFile(string relativePath, byte[] content)
        {
            return CreateFile(relativePath, content, File.WriteAllBytes);
        }

        public FileInfo RemoveAndDeleteFile(string relativePath)
        {
            var path = GetFullPath(relativePath);
            File.Delete(path);
            RemoveFile(relativePath);
            return new FileInfo(path);
        }

        protected FileInfo CreateFile<T>(string relativePath, T content, Action<string, T> writer)
        {
            var fullDir = GetFullPath(relativePath);
            var dir = Path.GetDirectoryName(fullDir);
            Directory.CreateDirectory(dir);
            writer(fullDir, content);
            return new FileInfo(fullDir);
        }

        private string GetFullPath(string relativePath)
        {
            var fullDir = Path.Combine(Path.GetDirectoryName(ProjectPath), relativePath);
            return fullDir;
        }

        public ProjectFileWriter WriteChanges()
        {
            File.WriteAllText(ProjectPath, GetXml());
            return this;
        }
    }
}
