using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Simple.Generator.Interfaces
{
    public class MethodSignature
    {
        public MethodInfo Method { get; protected set; }
        public ParameterInfo[] Parameters { get; protected set; }
        public Type ReturnType { get; protected set; }

        public IEnumerable<string> InvolvedNamespaces
        {
            get
            {
                return EnumerateInvolvedNamespaces().Distinct();
            }
        }

        private IEnumerable<string> EnumerateInvolvedNamespaces()
        {
            yield return ReturnType.Namespace;
            foreach (var parameter in Parameters)
                yield return parameter.ParameterType.Namespace;
        }

        public MethodSignature(MethodInfo method)
        {
            this.Method = method;
            this.Parameters = method.GetParameters();
            this.ReturnType = method.ReturnType;
        }

        public bool FirstParameterIs(Type type)
        {
            if (Parameters.Length == 0) return false;
            return Parameters[0].ParameterType == type;
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

        private void AppendTypeConstraints(StringBuilder str)
        {

        }

        private void AppendGenericDefinition(StringBuilder str)
        {
            if (Method.IsGenericMethod)
            {
                str.AppendFormat("<{0}>", string.Join(", ",
                    Method.GetGenericArguments().Select(x => x.GetRealClassName()).ToArray()));
            }
        }

        private void AppendParameters(StringBuilder str, int skip)
        {
            var parameters = Parameters.Skip(skip);
            str.AppendFormat("({0})",
                string.Join(", ",
                parameters.Select(x => string.Format("{0}{1} {2}", GetParameterModifiers(x), x.ParameterType.GetRealClassName(), x.Name)).ToArray()));
        }

        private void AppendMethodName(StringBuilder str)
        {
            str.AppendFormat(Method.Name);
        }

        private void AppendReturnType(StringBuilder str)
        {
            str.Append(ReturnType.GetRealClassName());
        }

        private string GetParameterModifiers(ParameterInfo x)
        {
            if (x.ParameterType.IsByRef)
                if (x.IsOut) return "out ";
                else return "ref ";
            else
                return string.Empty;

        }
    }
}
