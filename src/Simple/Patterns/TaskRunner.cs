using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Patterns
{
    public class TaskRunner
    {
        public class Helper
        {
            public string Message { get; set; }

            public void FailUnless(bool condition, string message)
            {
                if (!condition)
                    throw new ApplicationException(message);
            }

            public void WarnUnless(bool condition, string message)
            {
                if (!condition)
                    Message = message;
            }
        }

        [Serializable]
        public class Result
        {
            public enum Type
            {
                Success,
                Warning,
                Failure
            }

            public string ResultTypeTag
            {
                get { return ResultType.ToString().ToLower(); }
            }

            public string Description { get; set; }
            public string Message { get; set; }
            public Type ResultType { get; set; }
        }

        LinkedList<TaskRunner.Result> _results = new LinkedList<TaskRunner.Result>();
        public IEnumerable<TaskRunner.Result> Results { get { return _results; } }


        public void RunChained(string description, string sucessMessage, Func<TaskRunner.Helper, IEnumerable<TaskRunner.Result>> func)
        {
            IEnumerable<TaskRunner.Result> results = null;
            Run(description, sucessMessage, x => { results = func(x); });
            if (results != null) 
                foreach(var result in results)
                    _results.AddLast(result);
        }

        public void Run(string description, string successMessage, Action<TaskRunner.Helper> action)
        {
            var helper = new TaskRunner.Helper();
            try
            {
                action(helper);
                if (helper.Message == null)
                    _results.AddLast(new Result()
                    {
                        Description = description,
                        Message = successMessage,
                        ResultType = Result.Type.Success
                    });
                else
                    _results.AddLast(new Result()
                    {
                        Description = description,
                        Message = helper.Message,
                        ResultType = Result.Type.Warning
                    });
            }
            catch (Exception e)
            {
                _results.AddLast(new Result()
                {
                    Description = description,
                    Message = e.Message ?? "error",
                    ResultType = Result.Type.Failure
                });
            }
        }
    }
}
