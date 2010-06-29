using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Project.Generator.Data
{
    public interface ISampleSet
    {
        IEnumerable<string> Environments { get; }
        void Execute();
    }
}
