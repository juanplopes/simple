using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.NVelocity;
using Simple.Metadata;

namespace Simple.Generator
{

    public abstract class TableTemplateBase : SimpleTemplate, ITableTemplate
    {
        public bool Overwrite { get; protected set; }
        public ITableTemplate SetOverwrite(bool value)
        {
            Overwrite = value;
            return this;
        }

        public string FileTemplate { get; set; }
        public ProjectFileWriter Project { get; set; }
        public ITableTemplate Target(ProjectFileWriter project, string file)
        {
            Project = project;
            FileTemplate = file;
            return this;
        }

        public string FileType { get; set; }
        public ITableTemplate As(string type)
        {
            this.FileType = type;
            return this;
        }

        public void Create(DbTable table)
        {
            var conventions = GetConventions();
            var fileName = FileTemplate.AsFormat(conventions.NameFor(table));
            SetTemplate(table);
            if (Overwrite || !Project.ExistsFile(fileName))
                Project.AddNewFile(fileName, FileType, this.ToString());
        }

        public void Delete(string className)
        {
            var fileName = FileTemplate.AsFormat(className);
            Project.RemoveAndDeleteFile(fileName);
        }

        protected abstract ITableConventions GetConventions();
        protected abstract void SetTemplate(DbTable table);


        public TableTemplateBase(string template)
            : base(template)
        {
            FileType = ProjectFileWriter.Compile;
            Overwrite = true;
        }

    }
}
