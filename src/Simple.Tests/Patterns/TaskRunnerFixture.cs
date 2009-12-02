using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
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

            Assert.AreEqual(3, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success));
            Assert.AreEqual(3, runner.Results.Count(x => x.ResultTypeTag == "success"));
            Assert.AreEqual(3, runner.Results.Count(x => x.Message == "OK"));

            Assert.AreEqual(3, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
        }

        [Test]
        public void TestOneWarnedTask()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(2 + 2 == 4, "Erro"));
            runner.Run("Test2", "OK", x => x.WarnUnless(false, "Warn"));
            runner.Run("Test3", "OK", x => x.FailUnless(!false, "Erro"));

            Assert.AreEqual(2, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success));
            Assert.AreEqual(2, runner.Results.Count(x => x.ResultTypeTag == "success"));
            Assert.AreEqual(2, runner.Results.Count(x => x.Message == "OK"));

            Assert.AreEqual(1, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Warning));
            Assert.AreEqual(1, runner.Results.Count(x => x.ResultTypeTag == "warning"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Message == "Warn"));

            Assert.AreEqual(3, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
        }

        [Test]
        public void TestOneFailedTaskByFailUnless()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(2 + 2 == 4, "Erro"));
            runner.Run("Test2", "OK", x => x.FailUnless(false, "OMG"));
            runner.Run("Test3", "OK", x => x.FailUnless(!false, "Erro"));

            Assert.AreEqual(2, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success));
            Assert.AreEqual(2, runner.Results.Count(x => x.ResultTypeTag == "success"));
            Assert.AreEqual(2, runner.Results.Count(x => x.Message == "OK"));

            Assert.AreEqual(1, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure));
            Assert.AreEqual(1, runner.Results.Count(x => x.ResultTypeTag == "failure"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Message == "OMG"));

            Assert.AreEqual(3, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
        }

        [Test]
        public void TestOneFailedTaskByException()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(2 + 2 == 4, "Erro"));
            runner.Run("Test2", "OK", x => { throw new Exception("Whatever"); });
            runner.Run("Test3", "OK", x => x.FailUnless(!false, "Erro"));

            Assert.AreEqual(2, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success));
            Assert.AreEqual(2, runner.Results.Count(x => x.ResultTypeTag == "success"));
            Assert.AreEqual(2, runner.Results.Count(x => x.Message == "OK"));

            Assert.AreEqual(1, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure));
            Assert.AreEqual(1, runner.Results.Count(x => x.ResultTypeTag == "failure"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Message == "Whatever"));

            Assert.AreEqual(3, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
        }

        [Test]
        public void TestAllFailedTaskByException()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => { throw new Exception("Whatever"); });
            runner.Run("Test2", "OK", x => { throw new Exception("Whatever"); });
            runner.Run("Test3", "OK", x => { throw new Exception("Whatever"); });

            Assert.AreEqual(3, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure));
            Assert.AreEqual(3, runner.Results.Count(x => x.ResultTypeTag == "failure"));
            Assert.AreEqual(3, runner.Results.Count(x => x.Message == "Whatever"));

            Assert.AreEqual(3, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
        }

        [Test]
        public void TestAllFailedTaskByFailUnless()
        {
            TaskRunner runner = new TaskRunner();
            runner.Run("Test1", "OK", x => x.FailUnless(false, "OKOK"));
            runner.Run("Test2", "OK", x => x.FailUnless(false, "OKOK"));
            runner.Run("Test3", "OK", x => x.FailUnless(false, "OKOK"));

            Assert.AreEqual(3, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure));
            Assert.AreEqual(3, runner.Results.Count(x => x.ResultTypeTag == "failure"));
            Assert.AreEqual(3, runner.Results.Count(x => x.Message == "OKOK"));

            Assert.AreEqual(3, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
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

            Assert.AreEqual(4, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success));
            Assert.AreEqual(4, runner.Results.Count(x => x.ResultTypeTag == "success"));
            Assert.AreEqual(3, runner.Results.Count(x => x.Message == "OK"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Message == "OK!!!"));

            Assert.AreEqual(4, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test0"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
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

            Assert.AreEqual(3, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Failure));
            Assert.AreEqual(3, runner.Results.Count(x => x.ResultTypeTag == "failure"));
            Assert.AreEqual(3, runner.Results.Count(x => x.Message == "Erro"));

            Assert.AreEqual(1, runner.Results.Count(x => x.ResultType == TaskRunner.Result.Type.Success));
            Assert.AreEqual(1, runner.Results.Count(x => x.ResultTypeTag == "success"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Message == "OK!!!"));

            Assert.AreEqual(4, runner.Results.Count());
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test0"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test1"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test2"));
            Assert.AreEqual(1, runner.Results.Count(x => x.Description == "Test3"));
        }


    }
}
