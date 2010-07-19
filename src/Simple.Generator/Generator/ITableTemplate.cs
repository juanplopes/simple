using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Metadata;

namespace Simple.Generator
{
    public interface ITableTemplate
    {
        void Create(DbTable table);
        void Delete(string className);
        ITableTemplate As(string type);
        ITableTemplate SetOverwrite(bool value);
        ITableTemplate Target(ProjectFileWriter project, string file);
    }
}
