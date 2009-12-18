using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Migrator.Fluent
{
    public interface IAction
    {
        void Execute(ITransformationProvider provider);
    }
}
