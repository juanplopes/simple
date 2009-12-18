using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Migrator.Framework;

namespace Schema.Migrator
{
    public interface IAction
    {
        void Execute(ITransformationProvider provider);
    }
}
