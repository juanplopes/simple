using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Simple.Reflection;
using System.Globalization;

namespace Simple
{
    public static class ExpressionHelper
    {
        internal static IList<string> SplitProperty(this string propertyPath)
        {
            return propertyPath.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
        }


        public static string GetMemberName<T, P>(this Expression<Func<T, P>> expr)
        {
            return GetMemberName(expr.Body);
        }

        public static string GetMemberName(this Expression expr)
        {
            return string.Join(".", GetMemberPath(expr).ToArray());
        }

        public static IEnumerable<string> GetMemberPath<T, P>(this Expression<Func<T, P>> expr)
        {
            return GetMemberPath(expr.Body);
        }

        public static IEnumerable<string> GetMemberPath(this Expression expr)
        {
            if (expr != null && expr.NodeType == ExpressionType.Lambda)
                return (expr as LambdaExpression).Body.GetMemberPath();

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

        public static PropertyInfo GetProperty<T>(this string propertyPath)
        {
            return GetProperty(propertyPath, typeof(T));
        }

        public static PropertyInfo GetProperty<T>(this IEnumerable<string> propertyPath)
        {
            return GetProperty(propertyPath, typeof(T));
        }

        public static PropertyInfo GetProperty(this string propertyPath, Type type)
        {
            return GetProperty(propertyPath.SplitProperty(), type);
        }

        public static PropertyInfo GetProperty(this IEnumerable<string> propertyPath, Type type)
        {
            PropertyInfo ret = null;
            foreach (var prop in propertyPath)
            {
                ret = type.GetProperty(prop);
                if (ret == null) throw new ArgumentException("the specified property doesn't exist");

                type = ret.PropertyType;
            }
            return ret;
        }


        public static Expression GetPropertyExpression(this string propertyPath, Expression expr)
        {
            return propertyPath.SplitProperty().GetPropertyExpression(expr);
        }

      
        public static Expression GetPropertyExpression(this IEnumerable<string> propertyPath, Expression expr)
        {
            Expression ret = expr;

            foreach (var prop in propertyPath)
                ret = Expression.Property(ret, prop);

            return ret;
        }

        public static Expression<Func<T, object>> GetPropertyLambda<T>(this string propertyPath)
        {
            return propertyPath.SplitProperty().GetPropertyLambda<T>();
        }

        public static Expression<Func<T, object>> GetPropertyLambda<T>(this IEnumerable<string> propertyPath)
        {
            return propertyPath.GetPropertyLambda<T, object>();
        }

        public static Expression<Func<T, TProp>> GetPropertyLambda<T, TProp>(this string propertyPath)
        {
            return propertyPath.SplitProperty().GetPropertyLambda<T, TProp>();
        }

        public static Expression<Func<T, TProp>> GetPropertyLambda<T, TProp>(this IEnumerable<string> propertyPath)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            return Expression.Lambda<Func<T, TProp>>(propertyPath.GetPropertyExpression(parameter), parameter);
        }

        public static void SetValue(this MemberExpression expr, object target, object value)
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

            if (value != null && !prop.PropertyType.IsAssignableFrom(value.GetType()))
                if (value.GetType().CanAssign(typeof(IConvertible)))
                    value = Convert.ChangeType(value, prop.PropertyType.GetValueTypeIfNullable(), CultureInfo.InvariantCulture);
                else
                    throw new ArgumentException(string.Format("Don't know how to convert from {0} to {1}",
                        value.GetType().GetRealClassName(),
                        prop.PropertyType.GetRealClassName()));

            MethodCache.Do.GetSetter(prop)(target, value, null);
        }
    }
}
