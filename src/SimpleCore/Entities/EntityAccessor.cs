using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.ConfigSource;
using System.Linq.Expressions;
using Simple.Expressions.Editable;
using Simple.DataAccess;
using Simple.Services;
using NHibernate.Validator.Engine;
using Simple.Expressions;

namespace Simple.Entities
{
    internal static class ExpressionExtensions
    {
        public static EditableExpression ToEditable(this Expression expr)
        {
            return EditableExpression.CreateEditableExpression(expr);
        }
    }

    public class EntityAccessor<T, R> : AggregateFactory<EntityAccessor<T, R>>
        where R : IEntityService<T>
    {
        

    }
}
