using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Reflection;
using Simple.Reflection;

namespace Simple.Expressions
{
    public class ExpressionHelper
    {
        public static string GetMemberName<T, P>(Expression<Func<T, P>> expr)
        {
            return GetMemberName(expr.Body);
        }

        public static string GetMemberName(Expression expr)
        {
            return string.Join(".", GetMemberPath(expr).ToArray());
        }

        public static IEnumerable<string> GetMemberPath(Expression expr)
        {
            LinkedList<string> answer = new LinkedList<string>();
            while (expr != null &&  
                  (expr.NodeType == ExpressionType.MemberAccess ||
                  expr.NodeType == ExpressionType.Call ||
                  expr.NodeType == ExpressionType.Convert))
            {
                if (ExpressionType.MemberAccess == expr.NodeType)
                {
                    answer.AddFirst((expr as MemberExpression).Member.Name);
                    expr = (expr as MemberExpression).Expression;
                }
                else if (ExpressionType.Convert == expr.NodeType)
                {
                    expr = (expr as UnaryExpression).Operand;
                }
                else if (ExpressionType.Call == expr.NodeType)
                {
                    answer.AddFirst((expr as MethodCallExpression).Method.Name);
                    expr = (expr as MethodCallExpression).Object;
                }
            }
            return answer;
        }

        public static void SetValue(MemberExpression expr, object target, object value)
        {
            IEnumerable<string> path = GetMemberPath(expr);
            PropertyInfo prop = null;

            if (path.Count() == 0) throw new InvalidOperationException("Invalid lambda");

            foreach (var node in path)
            {
                if (prop != null)
                {
                    object temp = Activator.CreateInstance(prop.PropertyType);
                    MethodCache.Do.GetSetter(prop)(target, temp, null);
                    target = temp;
                }

                prop = target.GetType().GetProperty(node);
                if (prop == null) throw new InvalidOperationException("Cannot have method on the way");
            }

            MethodCache.Do.GetSetter(prop)(target,
                Convert.ChangeType(value, prop.PropertyType), null);
        }
    }
}
