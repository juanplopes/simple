using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Services;
using Simple.Patterns;

namespace Sample.Project.Services
{
    public interface ISystemService : IService
    {
        IList<TaskRunner.Result> Check();
    }
}
