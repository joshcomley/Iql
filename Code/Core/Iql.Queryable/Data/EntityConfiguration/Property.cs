using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using Iql.Queryable.Data.EntityConfiguration.Rules;
using Iql.Queryable.Data.EntityConfiguration.Rules.Display;
using Iql.Queryable.Data.EntityConfiguration.Validation;

namespace Iql.Queryable.Data.EntityConfiguration
{
    [DebuggerDisplay("{Name} - {Kind}")]
    public class Property<TOwner, TProperty, TElementType> : PropertyBase, IProperty
    {
        private Dictionary<string, object> _customInformation;

        public Property(
            string name, 
            bool nullable,
            bool isCollection, 
            Type declaringType,
            string convertedFromType,
            bool? readOnly,
            IqlType kind,
            IProperty countRelationship,
            Expression<Func<TOwner, TProperty>> propertyGetterExpression)
        {
            ConfigureProperty(
                name,
                nullable,
                isCollection, 
                declaringType, 
                typeof(TProperty),
                typeof(TElementType),
                kind,
                convertedFromType, 
                readOnly, 
                countRelationship, 
                propertyGetterExpression);
        }

        internal void ConfigureProperty(
            string name, 
            bool nullable,
            bool isCollection, 
            Type declaringType,
            Type propertyType,
            Type valueType,
            IqlType kind,
            string convertedFromType,
            bool? readOnly, 
            IProperty countRelationship, 
            Expression<Func<TOwner, TProperty>> propertyGetterExpression)
        {
            Name = name;
            if (readOnly.HasValue)
            {
                ReadOnly = readOnly.Value;
            }
            CountRelationship = countRelationship;
            if (countRelationship != null)
            {
                ((PropertyBase)countRelationship).CountRelationship = this;
            }
            Kind = 0;
            TypeDefinition = new TypeDetail(
                propertyType,
                nullable,
                declaringType,
                convertedFromType,
                valueType,
                isCollection,
                kind);

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

        public ValidationCollection<TOwner> ValidationRules { get; } = new ValidationCollection<TOwner>();
        IRuleCollection IProperty.ValidationRules => ValidationRules;

        public DisplayRuleCollection<TOwner> DisplayRules { get; } = new DisplayRuleCollection<TOwner>();
        IRuleCollection IProperty.DisplayRules => DisplayRules;

        public Expression<Func<TOwner, TProperty>> PropertyGetterExpressionTyped { get; set; }
        public Expression<Func<TOwner, TProperty, TProperty>> PropertySetterExpressionTyped { get; set; }
        public Func<TOwner, TProperty> PropertyGetterTyped { get; set; }
        public Func<TOwner, TProperty, TProperty> PropertySetterTyped { get; set; }
        public override Func<object, object> PropertyGetter { get; set; }
        public override Func<object, object, object> PropertySetter { get; set; }

        public Dictionary<string, object> CustomInformation => _customInformation = _customInformation ?? new Dictionary<string, object>();

        private static Expression<Func<T, TAssignmentProperty, TAssignmentProperty>> GetAssignmentLambda<T, TAssignmentProperty>(string name)
        {
            var p = Expression.Parameter(typeof(T), "o");
            var v = Expression.Parameter(typeof(T).GetProperty(name).PropertyType, "v");
            var l = Expression.Lambda(Expression.Assign(Expression.Property(p, name), v), p, v);
            return (Expression<Func<T, TAssignmentProperty, TAssignmentProperty>>)l;
        }
    }
}