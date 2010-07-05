using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple;
using Simple.Entities;

namespace Sample.Project.Tools.Templates
{
    public class AutoTypeInfos
    {
        public Type ServiceType { get; protected set; }
        public bool IsEntityService { get; protected set; }
        public Type EntityType { get; protected set; }

        public AutoTypeInfos(Type type)
        {
            ServiceType = type;
            IsEntityService = type.IsGenericType && type.GetGenericTypeDefinition()
                .CanAssign(typeof(IEntityService<>));
        }

    }
}
