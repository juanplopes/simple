using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Entities;

namespace Simple.Generator.Data
{
    public abstract class DevelopmentData<T> : DataList<T>
        where T : class, IEntity<T>, new()
    {
        public override IEnumerable<string> WillRunOn
        {
            get
            {
                yield return ConfigDef.Development;
            }
        }
    }
}
