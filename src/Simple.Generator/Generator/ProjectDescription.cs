using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Simple.Generator
{
    public class ProjectDescription
    {
        public string Name { get; protected set; }
        public string Directory { get; protected set; }
        public string ProjectFile { get; protected set; }
        public string Assembly { get; protected set; }

        public ProjectFileWriter Project
        {
            get
            {
                return new ProjectFileWriter(Path.Combine(Directory, ProjectFile));
            }
        }

        public ProjectDescription(string directory, string file, string assembly)
        {
            this.Directory = directory;
            this.ProjectFile = file;
            this.Assembly = assembly;
        }

        public ProjectDescription WithName(string name)
        {
            var newProject = new ProjectDescription(
                Directory.AsFormat(name),
                ProjectFile.AsFormat(name),
                Assembly.AsFormat(name));
            newProject.Name = name;

            return newProject;
        }
    }
}
