using Simple.Services;
using Example.Project.Services;
using System.Collections.Generic;
using Simple.Patterns;
using System;

namespace Example.Project.Services
{
    public partial interface ISystemService : IService, IService<ISystemService>
    {
        IList<TaskRunner.Result> Check();
        Int32 Test(Int32 value);
    }
}