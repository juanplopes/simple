using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Simple.Reflection;

namespace Simple.Reflection
{
    public class MethodSignature
    {
        public MethodInfo Method { get; protected set; }
        public HashSet<string> Namespaces { get { return TypeNames.Namespaces; } }
        protected TypeNameExtractor TypeNames { get; set; }

        public MethodSignature(MethodInfo method)
            : this(method, new HashSet<string>())
        {

        }

        public MethodSignature(MethodInfo method, HashSet<string> namespaces)
        {
            this.Method = method;
            this.TypeNames = new TypeNameExtractor(namespaces);
        }


        public string MakeCall(params string[] skipWith)
        {
            var str = new StringBuilder();
            AppendMethodName(str);
            AppendGenericDefinition(str);
            AppendParameters(str, skipWith);

            return str.ToString();
        }

        public string MakeSignature()
        {
            return MakeSignature(0);
        }

        public string MakeSignature(int skip)
        {
            var str = new StringBuilder();
            AppendReturnType(str);
            str.Append(' ');
            AppendMethodName(str);
            AppendGenericDefinition(str);
            AppendParameters(str, skip);
            AppendTypeConstraints(str);

            return str.ToString();
        }

        public bool FirstParameterIs(Type type)
        {
            var parameters = Method.GetParameters();
            if (parameters.Length == 0) return false;
            return parameters[0].ParameterType == type;
        }


        private void AppendTypeConstraints(StringBuilder str)
        {
            var typeParameters = Method.GetGenericArguments();
            str.Append(typeParameters.Select(x => CreateConstraintString(x)).StringJoin());
        }

        private string CreateConstraintString(Type x)
        {
            return new GenericSignature(x, TypeNames.Namespaces).MakeConstraints();
        }
       

        private void AppendGenericDefinition(StringBuilder str)
        {
            if (Method.IsGenericMethod)
            {
                str.AppendFormat("<{0}>", string.Join(", ",
                    Method.GetGenericArguments().Select(x => TypeNames.GetName(x)).ToArray()));
            }
        }

        private void AppendParameters(StringBuilder str, string[] skip)
        {
            var parameters = Method.GetParameters().Skip(skip.Length);
            str.AppendFormat("({0})",
                skip.Union(parameters.Select(x => string.Format("{0}{1}",
                    GetParameterModifiers(x, false), x.Name))).StringJoin(", "));
        }


        private void AppendParameters(StringBuilder str, int skip)
        {
            var parameters = Method.GetParameters().Skip(skip);
            str.AppendFormat("({0})",
                string.Join(", ",
                parameters.Select(x => string.Format("{0}{1} {2}", GetParameterModifiers(x, true), TypeNames.GetName(x.ParameterType), x.Name)).ToArray()));
        }

        private void AppendMethodName(StringBuilder str)
        {
            str.AppendFormat(Method.Name);
        }

        private void AppendReturnType(StringBuilder str)
        {
            str.Append(TypeNames.GetName(Method.ReturnType));
        }

        private string GetParameterModifiers(ParameterInfo x, bool includeParams)
        {
            if (x.ParameterType.IsByRef)
                if (x.IsOut) return "out ";
                else return "ref ";
            else if (x.IsDefined(typeof(ParamArrayAttribute), false) && includeParams)
                return "params ";
            else
                return string.Empty;



        }
    }
}
