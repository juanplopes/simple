using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using Simple.NVelocity;
using log4net.Appender;
using Simple;
using NVelocity.Util;

namespace Example.Project.Tools.Templates.View
{
    public class ViewGenerator : ICommand
    {
        public IList<string> ClassNames { get; set; }
        #region ICommand Members

        public void Execute()
        {
            using (var project = Options.Do.WebProject)
            {
                foreach (var className in ClassNames)
                {
                    Execute(project, ProjectFileWriter.Compile, "Controllers/{1}Controller.cs", className, Templates.Controller);
                    Execute(project, ProjectFileWriter.Content, "Views/{1}/Create.aspx", className, Templates.ViewCreate);
                    Execute(project, ProjectFileWriter.Content, "Views/{1}/Edit.aspx", className, Templates.ViewEdit);
                    Execute(project, ProjectFileWriter.Content, "Views/{1}/Details.aspx", className, Templates.ViewDetails);
                    Execute(project, ProjectFileWriter.Content, "Views/{1}/Index.aspx", className, Templates.ViewIndex);
                    Execute(project, ProjectFileWriter.Content, "Views/{1}/_Form.ascx", className, Templates.ViewForm);
                }
            }
        }

        protected void Execute(ProjectFileWriter project, string type, string fileFormat, string className, string template)
        {
            var className2 = Options.Do.Conventions.Pluralize(className);
            var file = fileFormat.AsFormat(className, className2);
            if (!project.ExistsFile(file))
                project.AddNewFile(file, type, GetTemplate(template, className).ToString());
        }


        protected SimpleTemplate GetTemplate(string content, string className)
        {
            var conventions = Options.Do.Conventions;
            var template = new SimpleTemplate(content);
            template["opt"] = Options.Do;
            template["classname"] = className;
            template["classname_2"] = conventions.Pluralize(className);
            return template;
        }

        #endregion
    }
}
