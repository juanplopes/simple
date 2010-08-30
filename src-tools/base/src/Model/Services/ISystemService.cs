using Simple.Services;
using Example.Project.Services;
using System.Collections.Generic;
using Simple.Patterns;

namespace Example.Project.Services
{
    public partial interface ISystemService : IService
    {
        IList<TaskRunner.Result> Check();
    }
}