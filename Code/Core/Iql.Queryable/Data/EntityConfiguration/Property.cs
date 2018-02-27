using System;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    [DebuggerDisplay("{Name} - {Kind}")]
    public class Property<TOwner, TProperty, TElementType> : PropertyBase, IProperty
    {
        public Property(
            string name, 
            bool isCollection, 
            Type declaringType,
            string convertedFromType,
            bool readOnly,
            IProperty countRelationship,
            Expression<Func<TOwner, TProperty>> propertyGetterExpression)
        {
            ConfigureProperty(
                name, 
                isCollection, 
                declaringType, 
                typeof(TProperty),
                typeof(TElementType),
                convertedFromType, 
                readOnly, 
                countRelationship, 
                propertyGetterExpression);
        }

        internal void ConfigureProperty(
            string name, 
            bool isCollection, 
            Type declaringType,
            Type propertyType,
            Type valueType,
            string convertedFromType,
            bool readOnly, 
            IProperty countRelationship, 
            Expression<Func<TOwner, TProperty>> propertyGetterExpression)
        {
            Configure(
                name,
                declaringType,
                propertyType,
                valueType,
                convertedFromType,
                isCollection,
                readOnly,
                countRelationship);

#if !TypeScript
            PropertyInfo = declaringType.GetProperty(name);
#endif
            PropertyGetterExpressionTyped = propertyGetterExpression;
            PropertyGetterTyped = propertyGetterExpression.Compile();
            PropertyGetter = o =>
            {
                var t = (TOwner) o;
                var propertyGetterTyped = PropertyGetterTyped;
                var value = propertyGetterTyped(t);
                return value;
            };

            PropertySetterExpressionTyped = GetAssignmentLambda<TOwner, TProperty>(name);
            PropertySetterTyped = PropertySetterExpressionTyped.Compile();
            PropertySetter = (o, v) => PropertySetterTyped((TOwner) o, (TProperty) v);
        }

        public ValidationCollection<TOwner> Validation { get; } = new ValidationCollection<TOwner>();
        IValidationCollection IProperty.Validation => Validation;

        public Expression<Func<TOwner, TProperty>> PropertyGetterExpressionTyped { get; set; }
        public Expression<Func<TOwner, TProperty, TProperty>> PropertySetterExpressionTyped { get; set; }
        public Func<TOwner, TProperty> PropertyGetterTyped { get; set; }
        public Func<TOwner, TProperty, TProperty> PropertySetterTyped { get; set; }
        public override Func<object, object> PropertyGetter { get; set; }
        public override Func<object, object, object> PropertySetter { get; set; }

        private static Expression<Func<T, TAssignmentProperty, TAssignmentProperty>> GetAssignmentLambda<T, TAssignmentProperty>(string name)
        {
            var p = Expression.Parameter(typeof(T), "o");
            var v = Expression.Parameter(typeof(T).GetProperty(name).PropertyType, "v");
            var l = Expression.Lambda(Expression.Assign(Expression.Property(p, name), v), p, v);
            return (Expression<Func<T, TAssignmentProperty, TAssignmentProperty>>)l;
        }
    }
}