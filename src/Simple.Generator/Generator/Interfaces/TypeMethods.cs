using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Generator.Interfaces
{
    public class TypeMethods
    {
        public Type Type { get; protected set; }
        public TypeMethods(Type type)
        {
            Type = type;
        }

        public IEnumerable<MethodSignature> Methods
        {
            get
            {
                foreach (var method in Type.GetMethods(
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    yield return new MethodSignature(method);
            }
        }



    }
}
