using System;
using System.Linq;
using NUnit.Framework;
using SharpTestsEx;
using Simple.Patterns;

namespace Simple.Tests.Patterns
{
    [TestFixture]
    public class TaskRunnerFixture
    {
        [Test]
        public void TestAllTasksSuccessful()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(2 + 2 == 4, "Erro"));
            runner.Run("Test2", "OK", x => x.FailUnless(true, "Erro"));
            runner.Run("Test3", "OK", x => x.FailUnless(!false, "Erro"));

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success).Should().Be(3);
            runner.Results.Count(x => x.ResultTypeTag == "success").Should().Be(3);
            runner.Results.Count(x => x.Message == "OK").Should().Be(3);

            runner.Results.Count().Should().Be(3);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }

        [Test]
        public void TestOneWarnedTask()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(2 + 2 == 4, "Erro"));
            runner.Run("Test2", "OK", x => x.WarnUnless(false, "Warn"));
            runner.Run("Test3", "OK", x => x.FailUnless(!false, "Erro"));

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success).Should().Be(2);
            runner.Results.Count(x => x.ResultTypeTag == "success").Should().Be(2);
            runner.Results.Count(x => x.Message == "OK").Should().Be(2);

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Warning).Should().Be(1);
            runner.Results.Count(x => x.ResultTypeTag == "warning").Should().Be(1);
            runner.Results.Count(x => x.Message == "Warn").Should().Be(1);

            runner.Results.Count().Should().Be(3);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }

        [Test]
        public void TestOneFailedTaskByFailUnless()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(2 + 2 == 4, "Erro"));
            runner.Run("Test2", "OK", x => x.FailUnless(false, "OMG"));
            runner.Run("Test3", "OK", x => x.FailUnless(!false, "Erro"));

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success).Should().Be(2);
            runner.Results.Count(x => x.ResultTypeTag == "success").Should().Be(2);
            runner.Results.Count(x => x.Message == "OK").Should().Be(2);

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure).Should().Be(1);
            runner.Results.Count(x => x.ResultTypeTag == "failure").Should().Be(1);
            runner.Results.Count(x => x.Message == "OMG").Should().Be(1);

            runner.Results.Count().Should().Be(3);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }

        [Test]
        public void TestOneFailedTaskByException()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(2 + 2 == 4, "Erro"));
            runner.Run("Test2", "OK", x => { throw new Exception("Whatever"); });
            runner.Run("Test3", "OK", x => x.FailUnless(!false, "Erro"));

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success).Should().Be(2);
            runner.Results.Count(x => x.ResultTypeTag == "success").Should().Be(2);
            runner.Results.Count(x => x.Message == "OK").Should().Be(2);

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure).Should().Be(1);
            runner.Results.Count(x => x.ResultTypeTag == "failure").Should().Be(1);
            runner.Results.Count(x => x.Message == "Whatever").Should().Be(1);

            runner.Results.Count().Should().Be(3);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }

        [Test]
        public void TestAllFailedTaskByException()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => { throw new Exception("Whatever"); });
            runner.Run("Test2", "OK", x => { throw new Exception("Whatever"); });
            runner.Run("Test3", "OK", x => { throw new Exception("Whatever"); });

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure).Should().Be(3);
            runner.Results.Count(x => x.ResultTypeTag == "failure").Should().Be(3);
            runner.Results.Count(x => x.Message == "Whatever").Should().Be(3);

            runner.Results.Count().Should().Be(3);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }

        [Test]
        public void TestAllFailedTaskByFailUnless()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(false, "OKOK"));
            runner.Run("Test2", "OK", x => x.FailUnless(false, "OKOK"));
            runner.Run("Test3", "OK", x => x.FailUnless(false, "OKOK"));

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure).Should().Be(3);
            runner.Results.Count(x => x.ResultTypeTag == "failure").Should().Be(3);
            runner.Results.Count(x => x.Message == "OKOK").Should().Be(3);

            runner.Results.Count().Should().Be(3);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }

        [Test]
        public void TestRunChainedWithSuccess()
        {
            TaskRunner runner = new TaskRunner();

            runner.RunChained("Test0", "OK!!!", x =>
            {
                TaskRunner runner2 = new TaskRunner();
                runner.Run("Test1", "OK", y => y.FailUnless(2 + 2 == 4, "Erro"));
                runner.Run("Test2", "OK", y => y.FailUnless(true, "Erro"));
                runner.Run("Test3", "OK", y => y.FailUnless(!false, "Erro"));
                return runner2.Results;
            });

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success).Should().Be(4);
            runner.Results.Count(x => x.ResultTypeTag == "success").Should().Be(4);
            runner.Results.Count(x => x.Message == "OK").Should().Be(3);
            runner.Results.Count(x => x.Message == "OK!!!").Should().Be(1);

            runner.Results.Count().Should().Be(4);
            runner.Results.Count(x => x.Description == "Test0").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }

        [Test]
        public void TestRunChainedWithFailures()
        {
            TaskRunner runner = new TaskRunner();

            runner.RunChained("Test0", "OK!!!", x =>
            {
                TaskRunner runner2 = new TaskRunner();
                runner.Run("Test1", "OK", y => y.FailUnless(2 + 2 != 4, "Erro"));
                runner.Run("Test2", "OK", y => y.FailUnless(false, "Erro"));
                runner.Run("Test3", "OK", y => y.FailUnless(!true, "Erro"));
                return runner2.Results;
            });

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure).Should().Be(3);
            runner.Results.Count(x => x.ResultTypeTag == "failure").Should().Be(3);
            runner.Results.Count(x => x.Message == "Erro").Should().Be(3);

            runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success).Should().Be(1);
            runner.Results.Count(x => x.ResultTypeTag == "success").Should().Be(1);
            runner.Results.Count(x => x.Message == "OK!!!").Should().Be(1);

            runner.Results.Count().Should().Be(4);
            runner.Results.Count(x => x.Description == "Test0").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test1").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test2").Should().Be(1);
            runner.Results.Count(x => x.Description == "Test3").Should().Be(1);
        }


    }
}
