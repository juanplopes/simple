using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Generator;

namespace Simple.Tests.Generator
{
    public class ProjectWriter
    {
        [Test]
        public void AddingDuplicatedFileWontCreateNew()
        {
            var project = CsProjects.SampleProjectSimple;
            var writer = new ProjectWriter(project);
            writer.AddCompile(@"Common\EnumerableExtensions.cs");

            var newProject = writer.GetXml();

            Assert.AreEqual(
                newProject.IndexOf("Include=\"Common\\EnumerableExtensions.cs\" />"),
                newProject.LastIndexOf("Include=\"Common\\EnumerableExtensions.cs\" />"), "two occurrences");
        }

        [Test]
        public void AddingDuplicatedFileWillMaintainTheLast()
        {
            var project = CsProjects.SampleProjectSimple;
            var writer = new ProjectWriter(project);
            writer.AddEmbeddedResource(@"Common\EnumerableExtensions.cs");

            var newProject = writer.GetXml();

            StringAssert.Contains("<Compile Include=\"Common\\EnumerableExtensions.cs\" />", newProject);
            StringAssert.DoesNotContain("<EmbeddedResource Include=\"Common\\EnumerableExtensions.cs\" />", newProject);
        }


        [Test]
        public void CanAddCompileFileInProject()
        {
            var project = CsProjects.SampleProjectSimple;
            var writer = new ProjectWriter(project);
            writer.AddCompile("__test__");

            var newProject = writer.GetXml();

            StringAssert.Contains("<Compile Include=\"__test__\" />", newProject);
        }

        [Test]
        public void CanAddEmbeddedResourceFileInProject()
        {
            var project = CsProjects.SampleProjectSimple;
            var writer = new ProjectWriter(project);
            writer.AddEmbeddedResource("__test__");

            var newProject = writer.GetXml();

            StringAssert.Contains("<EmbeddedResource Include=\"__test__\" />", newProject);
        }

        [Test]
        public void CanRemoveEnumerableExtensionsInProject()
        {
            var project = CsProjects.SampleProjectSimple;
            StringAssert.Contains("<Compile Include=\"Common\\EnumerableExtensions.cs\" />", project);

            var writer = new ProjectWriter(project);
            writer.RemoveFile(@"Common\EnumerableExtensions.cs");
            var newProject = writer.GetXml();

            StringAssert.DoesNotContain("<Compile Include=\"Common\\EnumerableExtensions.cs\" />", newProject);
        }
    }
}
