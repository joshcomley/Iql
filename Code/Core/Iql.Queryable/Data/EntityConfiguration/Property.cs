using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    [DebuggerDisplay("{Name} - {Kind}")]
    public class Property<TOwner, TProperty, TValueType> : PropertyBase
    {
        public Property(
            string name, 
            bool isCollection, 
            Type declaringType,
            string convertedFromType,
            bool readOnly,
            IProperty countRelationship,
            Expression<Func<TOwner, TValueType>> propertyGetterExpression) : base(
                name, 
                typeof(TProperty), 
                isCollection, 
                declaringType, 
                convertedFromType, 
                readOnly, 
                countRelationship)
        {
            PropertyGetterTyped = propertyGetterExpression.Compile();
            PropertyGetter = o => PropertyGetterTyped((TOwner) o);
        }
        public Expression<Func<TOwner, TValueType>> PropertyGetterExpressionTyped { get; set; }
        public Func<TOwner, TValueType> PropertyGetterTyped { get; set; }
        public override Func<object, object> PropertyGetter { get; }
    }
}