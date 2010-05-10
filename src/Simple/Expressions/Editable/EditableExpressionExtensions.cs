using System.Collections.Generic;

namespace Simple.Expressions.Editable
{
    public static class EditableExpressionExtension
    {
        public static IEnumerable<EditableExpression> LinkNodes(this EditableExpression source)
        {
            //returns all the "paths" or "links" from a node in an expression tree
            //  each expression type that has "links" from it has different kinds of links
            if (source is EditableLambdaExpression)
            {
                yield return (source as EditableLambdaExpression).Body;
                foreach (EditableParameterExpression parm in (source as EditableLambdaExpression).Parameters)
                    yield return parm;
            }
            else if (source is EditableBinaryExpression)
            {
                yield return (source as EditableBinaryExpression).Left;
                yield return (source as EditableBinaryExpression).Right;
            }
            else if (source is EditableConditionalExpression)
            {
                yield return (source as EditableConditionalExpression).IfTrue;
                yield return (source as EditableConditionalExpression).IfFalse;
                yield return (source as EditableConditionalExpression).Test;
            }
            else if (source is EditableInvocationExpression)
                foreach (EditableExpression x in (source as EditableInvocationExpression).Arguments)
                    yield return x;
            else if (source is EditableListInitExpression)
            {
                yield return (source as EditableListInitExpression).NewExpression;
                foreach (EditableElementInit x in (source as EditableListInitExpression).Initializers)
                    foreach (EditableExpression ex in x.Arguments)
                        yield return ex;
            }
            else if (source is EditableMemberExpression)
                yield return (source as EditableMemberExpression).Expression;
            else if (source is EditableMemberInitExpression)
                yield return (source as EditableMemberInitExpression).NewExpression;
            else if (source is EditableMethodCallExpression)
            {
                foreach (EditableExpression x in (source as EditableMethodCallExpression).Arguments)
                    yield return x;
                if ((source as EditableMethodCallExpression).Object != null)
                {
                    yield return (source as EditableMethodCallExpression).Object;
                }
            }
            else if (source is EditableNewArrayExpression)
                foreach (EditableExpression x in (source as EditableNewArrayExpression).Expressions)
                    yield return x;
            else if (source is EditableNewExpression)
                foreach (EditableExpression x in (source as EditableNewExpression).Arguments)
                    yield return x;
            else if (source is EditableTypeBinaryExpression)
                yield return (source as EditableTypeBinaryExpression).Expression;
            else if (source is EditableUnaryExpression)
                yield return (source as EditableUnaryExpression).Operand;
        }

        //return all the nodes in a given expression tree
        public static IEnumerable<EditableExpression> Nodes(this EditableExpression source)
        {
            //i.e. left, right, body, etc.
            foreach (EditableExpression linkNode in source.LinkNodes())
                //recursive call to get the nodes from the tree, until you hit terminals
                foreach (EditableExpression subNode in linkNode.Nodes())
                    yield return subNode;
            yield return source; //return the root of this most recent call
        }
    }
}
