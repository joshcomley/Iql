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
            PropertyGetterExpressionTyped = propertyGetterExpression;
            PropertyGetterTyped = propertyGetterExpression.Compile();
            PropertyGetter = o => PropertyGetterTyped((TOwner) o);

            PropertySetterExpressionTyped = GetAssignmentLambda<TOwner, TValueType>(name);
            PropertySetterTyped = PropertySetterExpressionTyped.Compile();
            PropertySetter = (o, v) => PropertySetterTyped((TOwner) o, (TValueType) v);
        }
        public Expression<Func<TOwner, TValueType>> PropertyGetterExpressionTyped { get; set; }
        public Expression<Func<TOwner, TValueType, TValueType>> PropertySetterExpressionTyped { get; set; }
        public Func<TOwner, TValueType> PropertyGetterTyped { get; set; }
        public Func<TOwner, TValueType, TValueType> PropertySetterTyped { get; set; }
        public override Func<object, object> PropertyGetter { get; }
        public override Func<object, object, object> PropertySetter { get; }

        private static Expression<Func<T, TProperty, TProperty>> GetAssignmentLambda<T, TProperty>(string name)
        {
            var p = Expression.Parameter(typeof(T), "o");
            var v = Expression.Parameter(typeof(T).GetProperty(name).PropertyType, "v");
            var l = Expression.Lambda(Expression.Assign(Expression.Property(p, name), v), p, v);
            return (Expression<Func<T, TProperty, TProperty>>)l;
        }
    }
}