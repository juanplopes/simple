using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace Simple.Expressions.Editable
{
    public static class ExpressionExtension
    {
        public static IEnumerable<Expression> LinkNodes(this Expression source)
        {
            //returns all the "paths" or "links" from a node in an expression tree
            //  each expression type that has "links" from it has different kinds of links
            if (source is LambdaExpression)
            {
                yield return (source as LambdaExpression).Body;
                foreach (ParameterExpression parm in (source as LambdaExpression).Parameters)
                    yield return parm;
            }
            else if (source is BinaryExpression)
            {
                yield return (source as BinaryExpression).Left;
                yield return (source as BinaryExpression).Right;
            }
            else if (source is ConditionalExpression)
            {
                yield return (source as ConditionalExpression).IfTrue;
                yield return (source as ConditionalExpression).IfFalse;
                yield return (source as ConditionalExpression).Test;
            }
            else if (source is InvocationExpression)
            {
                foreach (Expression x in (source as InvocationExpression).Arguments)
                    yield return x;

                yield return (source as InvocationExpression).Expression;
            }
            else if (source is ListInitExpression)
            {
                yield return (source as ListInitExpression).NewExpression;
            }
            else if (source is MemberExpression)
                yield return (source as MemberExpression).Expression;
            else if (source is MemberInitExpression)
                yield return (source as MemberInitExpression).NewExpression;
            else if (source is MethodCallExpression)
            {
                foreach (Expression x in (source as MethodCallExpression).Arguments)
                    yield return x;
                if ((source as MethodCallExpression).Object != null)
                {
                    yield return (source as MethodCallExpression).Object;
                }
            }
            else if (source is NewArrayExpression)
                foreach (Expression x in (source as NewArrayExpression).Expressions)
                    yield return x;
            else if (source is NewExpression)
                foreach (Expression x in (source as NewExpression).Arguments)
                    yield return x;
            else if (source is TypeBinaryExpression)
                yield return (source as TypeBinaryExpression).Expression;
            else if (source is UnaryExpression)
                yield return (source as UnaryExpression).Operand;
        }

        //return all the nodes in a given expression tree
        public static IEnumerable<Expression> Nodes(this Expression source)
        {
            //i.e. left, right, body, etc.
            foreach (Expression linkNode in source.LinkNodes())
                //recursive call to get the nodes from the tree, until you hit terminals
                foreach (Expression subNode in linkNode.Nodes())
                    yield return subNode;
            yield return source; //return the root of this most recent call
        }
    }
}
