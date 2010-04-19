using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableMemberInitExpression : EditableExpressionImpl<MemberInitExpression>
    {
        // Properties
        public EditableNewExpression NewExpression
        {
            get;
            set;
        }

        public EditableMemberBindingCollection Bindings
        {
            get;
            set;
        }
        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.MemberInit; }
            set { }
        }

        // Ctors
        public EditableMemberInitExpression()
        {
            Bindings = new EditableMemberBindingCollection();
        }

        public EditableMemberInitExpression(MemberInitExpression membInit)
            : this(EditableExpression.Create(membInit.NewExpression) as EditableNewExpression, membInit.Bindings)
        {
        }

        public EditableMemberInitExpression(EditableNewExpression newEx, IEnumerable<MemberBinding> bindings)
        {
            Bindings = new EditableMemberBindingCollection(bindings);
            NewExpression = newEx;
        }

        public EditableMemberInitExpression(NewExpression newRawEx, IEnumerable<MemberBinding> bindings)
            : this(EditableExpression.Create(newRawEx) as EditableNewExpression, bindings)
        {
        }

        // Methods
        public override MemberInitExpression ToTypedExpression()
        {
            return Expression.MemberInit(NewExpression.ToExpression() as NewExpression, Bindings.GetMemberBindings());
        }
    }
}
