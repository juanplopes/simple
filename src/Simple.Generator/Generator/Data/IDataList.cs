using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Generator.Data
{
    public interface IDataList
    {
        bool Matches(string environment);
        void Execute();
    }
}
