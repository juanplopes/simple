using Sample.Project.Services;
using Simple.Services;
using System.Collections.Generic;
using Simple.Patterns;

namespace Sample.Project.Services
{
    public partial interface ISystemService : IService
    {
        IList<TaskRunner.Result> Check();
    }
}