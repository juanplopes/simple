using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;

namespace Simple.Expressions.Editable
{
    [Serializable]
    public partial class EditableNewExpression : EditableExpressionImpl<NewExpression>
    {
        // Properties                
        [IgnoreDataMember]
        public ConstructorInfo Constructor
        {
            get
            {
                return ReflectionExtensions.FromConstructorSerializableForm(ConstructorName);
            }
            set
            {
                ConstructorName = value.ToSerializableForm();
            }
        }

        public string ConstructorName { get; set; }


        public EditableExpressionCollection Arguments
        {
            get;
            set;
        }

        public EditableMemberInfoCollection Members
        {
            get;
            set;
        }

        
        public override ExpressionType NodeType
        {
            get { return ExpressionType.New; }
            set { }
        }

        // Ctors
        public EditableNewExpression()
        {
        }

        public EditableNewExpression(NewExpression newEx)
            : this(newEx.Constructor, new EditableExpressionCollection(newEx.Arguments), newEx.Members, newEx.Type)
        { }

        public EditableNewExpression(ConstructorInfo constructor, IEnumerable<EditableExpression> arguments, IEnumerable<MemberInfo> members, Type type)
            : this(constructor, new EditableExpressionCollection(arguments), members, type)
        { }

        public EditableNewExpression(ConstructorInfo constructor, EditableExpressionCollection arguments, IEnumerable<MemberInfo> members, Type type)
            : base(type)
        {
            Arguments = arguments;
            Constructor = constructor;            
            Members = new EditableMemberInfoCollection(members);
        }

        // Methods
        public override NewExpression ToTypedExpression()
        {
            if (Constructor != null)
                return Expression.New(Constructor, Arguments.GetExpressions());
            else
                return Expression.New(Type);
        }
    }
}
