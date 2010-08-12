using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SharpTestsEx;
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
        public void CanAddAlreadyExistingFileWontWriteChanges()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");
            writer.WriteChanges();

            using (new FileInfo(writer.ProjectPath).Open(FileMode.Open, FileAccess.ReadWrite))
            {
                writer.AddNewCompile("asd/qwe/simsim.txt", "olá");
                writer.WriteChanges();
            }
        }

        [Test]
        public void CanOpenFileUsingPatternOnly()
        {
            var writer = new ProjectFileWriter("test/te??.csproj");
            writer.ProjectPath.Should().Be(Path.GetFullPath("test/test.csproj"));
        }

        [Test]
        public void CanOpenFileUsingPatternWithNoDirectory()
        {
            Environment.CurrentDirectory = Environment.CurrentDirectory + "/test";
            var writer = new ProjectFileWriter("te??.csproj");
            writer.ProjectPath.Should().Be(Path.GetFullPath("test.csproj"));
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void CannotOpenFileUsingPatternOnlyWhenItDoesntExist()
        {
            var writer = new ProjectFileWriter("test/te?.csproj");
            writer.ProjectPath.Should().Be(Path.GetFullPath("test/test.csproj"));
        }

        [Test, ExpectedException(typeof(FileNotFoundException))]
        public void CannotOpenFileUsingPatternWithNoDirectoryWhenItDoesntExist()
        {
            Environment.CurrentDirectory = Environment.CurrentDirectory + "/test";
            var writer = new ProjectFileWriter("te?.csproj");
            writer.ProjectPath.Should().Be(Path.GetFullPath("test.csproj"));
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
            File.ReadAllText("test/asd/qwe/simsim.txt").Should().Be("olá");
        }


        [Test]
        public void CannotAddCompileFileToProjectWithoutWriteChanges()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            StringAssert.DoesNotContain(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            File.ReadAllText("test/asd/qwe/simsim.txt").Should().Be("olá");
        }

        [Test]
        public void CanAddCompileFileToProjectWithoutWriteChangesWithDisposeButWithManualCommit()
        {
            using (var writer = new ProjectFileWriter("test/test.csproj").ManualCommit())
                writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            StringAssert.DoesNotContain(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            File.ReadAllText("test/asd/qwe/simsim.txt").Should().Be("olá");

        }


        [Test]
        public void CanAddCompileFileToProjectWithoutWriteChanges()
        {
            using (var writer = new ProjectFileWriter("test/test.csproj"))
                writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            File.ReadAllText("test/asd/qwe/simsim.txt").Should().Be("olá");

        }

        [Test]
        public void CanAddCompileFileToProjectWithoutWriteChangesWithDisposeButWithAutoCommit()
        {
            using (var writer = new ProjectFileWriter("test/test.csproj").AutoCommit())
                writer.AddNewCompile("asd/qwe/simsim.txt", "olá");

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            File.ReadAllText("test/asd/qwe/simsim.txt").Should().Be("olá");
        }

        [Test]
        public void CanAddCompileFileToProjectWorkEvenIfWeChangeDir()
        {
            var writer = new ProjectFileWriter("test/test.csproj");

            Environment.CurrentDirectory = dir.ToString();
            writer.AddNewCompile("asd/qwe/simsim.txt", "olá");
            writer.WriteChanges();

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test.csproj"));
            File.ReadAllText("asd/qwe/simsim.txt").Should().Be("olá");
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
            File.ReadAllText("test/asd/qwe/simsim.txt").Should().Be("olá");
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
            File.Exists("test/asd/qwe/simsim.txt").Should().Be.False();
        }

        [Test]
        public void CanAddCompileBinaryFileToProject()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            writer.AddNewCompile("asd/qwe/simsim.txt", new byte[] { 1, 2, 3 });
            writer.WriteChanges();

            StringAssert.Contains(@"<Compile Include=""asd\qwe\simsim.txt"" />", File.ReadAllText("test/test.csproj"));
            File.ReadAllBytes("test/asd/qwe/simsim.txt").Should().Have.SameSequenceAs<byte>(1, 2, 3);
        }

        [Test]
        public void CanResolveFullPathInsideAProject()
        {
            var writer = new ProjectFileWriter("test/test.csproj");
            var path = writer.GetFullPath("asd/qwe/simsim.txt");
            StringAssert.EndsWith(@"test\asd\qwe\simsim.txt", path);

        }
    }
}
