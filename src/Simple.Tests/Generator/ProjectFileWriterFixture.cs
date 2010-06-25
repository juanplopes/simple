using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using Simple.Generator;

namespace Simple.Tests.Generator
{
    public class ProjectFileWriterFixture
    {
        DirectoryInfo dir = null;
        string currDir = null;
        [SetUp]
        public void Setup()
        {
            currDir = Environment.CurrentDirectory;

            Assert.False(Directory.Exists("test"));
            dir = new DirectoryInfo("test");

            Directory.CreateDirectory("test");
            File.WriteAllText("test/test.csproj", CsProjects.SampleProjectSimple);
        }

        [TearDown]
        public void Teardown()
        {
            Environment.CurrentDirectory = currDir;
            if (dir != null)
            {
                Assert.True(dir.Exists);
                dir.Delete(true);
            }
        }

        [Test]
        public void CanAddCompileFileToProject()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");
            writer.WriteChanges();

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.AreEqual("olá", File.ReadAllText("test/asd/qwe/simsim.txt"));
        }

        [Test]
        public void CanCheckIfFileExists()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            
            Assert.False(writer.ExistsFile("asd/qwe/simsim.txt"));
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            Assert.True(writer.ExistsFile("asd/qwe/simsim.txt"));
            writer.WriteChanges();
        }

        [Test]
        public void CanAddCrazyFileToProject()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewFile("asd/qwe/simsim.txt", "ASDASD", "olá");
            writer.WriteChanges();

            StringAssert.Contains(@"<ASDASD Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.AreEqual("olá", File.ReadAllText("test/asd/qwe/simsim.txt"));
        }


        [Test]
        public void CannotAddCompileFileToProjectWithoutWriteChanges()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            StringAssert.DoesNotContain(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.AreEqual("olá", File.ReadAllText("test/asd/qwe/simsim.txt"));
        }

        [Test]
        public void CanAddCompileFileToProjectWithoutWriteChangesWithDisposeButWithoutAutoCommit()
        {
            using (var writer = new ProjectFileWriter("test/test.csproj"))
                writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            StringAssert.DoesNotContain(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.AreEqual("olá", File.ReadAllText("test/asd/qwe/simsim.txt"));

        }

        [Test]
        public void CanAddCompileFileToProjectWithoutWriteChangesWithDisposeButWithAutoCommit()
        {
            using (var writer = new ProjectFileWriter("test/test.csproj").AutoCommit())
                writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.AreEqual("olá", File.ReadAllText("test/asd/qwe/simsim.txt"));
        }

        [Test]
        public void CanAddCompileFileToProjectWorkEvenIfWeChangeDir()
        {
            var writer = new ProjectFileWriter("test/test.csproj");

            Environment.CurrentDirectory = dir.ToString();
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");
            writer.WriteChanges();

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test.csproj"));
            Assert.AreEqual("olá", File.ReadAllText("asd/qwe/simsim.txt"));
        }

        [Test]
        public void AddCompileFileToProjectThenRemoveItWontDeleteTheFile()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");
            writer.WriteChanges();

            writer.RemoveFile("asd/qwe/simsim.txt");
            writer.WriteChanges();

            StringAssert.DoesNotContain(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.AreEqual("olá", File.ReadAllText("test/asd/qwe/simsim.txt"));
        }

        [Test]
        public void AddCompileFileToProjectThenRemoveAndDeleteWillDeleteTheFile()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");
            writer.WriteChanges();

            writer.RemoveAndDeleteFile("asd/qwe/simsim.txt");
            writer.WriteChanges();

            StringAssert.DoesNotContain(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.IsFalse(File.Exists("test/asd/qwe/simsim.txt"));
        }

        [Test]
        public void CanAddCompileBinaryFileToProject()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", new byte[] { 1, 2, 3 });
            writer.WriteChanges();

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            Assert.AreEqual(new byte[] { 1, 2, 3 }, File.ReadAllBytes("test/asd/qwe/simsim.txt"));
        }
    }
}
