using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Migrator.Framework;

namespace Simple.Migrator.Fluent
{
    public interface IAction
    {
        void Execute(ITransformationProvider provider);
    }
}
