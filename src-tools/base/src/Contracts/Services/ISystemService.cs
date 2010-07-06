using System.Collections.Generic;
using Simple.Patterns;

namespace Sample.Project.Services
{
    public partial interface ISystemService : Simple.Services.IService
    {
        IList<TaskRunner.Result> Check();
    }
}