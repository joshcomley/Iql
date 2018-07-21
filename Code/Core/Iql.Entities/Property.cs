using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace Iql.Entities
{
    [DebuggerDisplay("{Name} - {Kind}")]
    public class Property<TOwner, TProperty, TElementType> : PropertyBase, IEntityProperty<TOwner>
        where TOwner : class
    {
        private Dictionary<string, object> _customInformation;
        private MediaKey<TOwner> _mediaKey;

        public Property(
            IEntityConfiguration entityConfiguration,
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
                entityConfiguration,
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
            IEntityConfiguration entityConfiguration,
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
            EntityConfiguration = entityConfiguration;
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
            GetValue = o =>
            {
                var t = (TOwner)o;
                var propertyGetterTyped = PropertyGetterTyped;
                var value = propertyGetterTyped(t);
                return value;
            };

            PropertySetterExpressionTyped = GetAssignmentLambda<TOwner, TProperty>(name);
            PropertySetterTyped = PropertySetterExpressionTyped.Compile();
            SetValue = (o, v) => PropertySetterTyped((TOwner)o, (TProperty)v);
        }

        public new ValidationCollection<TOwner> ValidationRules { get; private set; } = new ValidationCollection<TOwner>();
        IRuleCollection<IBinaryRule> IPropertyMetadata.ValidationRules
        {
            get => ValidationRules;
            set => ValidationRules = (ValidationCollection<TOwner>)value;
        }

        public new DisplayRuleCollection<TOwner> DisplayRules { get; private set; } = new DisplayRuleCollection<TOwner>();
        IRuleCollection<IDisplayRule> IPropertyMetadata.DisplayRules
        {
            get => DisplayRules;
            set => DisplayRules = (DisplayRuleCollection<TOwner>)value;
        }

        public new RelationshipRuleCollection<TOwner, TElementType> RelationshipFilterRules { get; private set; } = new RelationshipRuleCollection<TOwner, TElementType>();

        protected override IMediaKey GetMediaKey()
        {
            return _mediaKey;
        }

        protected override void SetMediaKey(IMediaKey value)
        {
            _mediaKey = (MediaKey<TOwner>) value;
        }

        public new MediaKey<TOwner> MediaKey
        {
            get => _mediaKey = _mediaKey ?? new MediaKey<TOwner>(this);
            set => _mediaKey = value;
        }

        IMediaKey IPropertyMetadata.MediaKey
        {
            get => MediaKey;
            set => MediaKey = (MediaKey<TOwner>)value;
        }

        IRuleCollection<IRelationshipRule> IPropertyMetadata.RelationshipFilterRules
        {
            get => RelationshipFilterRules;
            set => RelationshipFilterRules = (RelationshipRuleCollection<TOwner, TElementType>)value;
        }

        public Expression<Func<TOwner, TProperty>> PropertyGetterExpressionTyped { get; set; }
        public Expression<Func<TOwner, TProperty, TProperty>> PropertySetterExpressionTyped { get; set; }
        public Func<TOwner, TProperty> PropertyGetterTyped { get; set; }
        public Func<TOwner, TProperty, TProperty> PropertySetterTyped { get; set; }
        public override Func<object, object> GetValue { get; set; }
        public override Func<object, object, object> SetValue { get; set; }

        public Dictionary<string, object> CustomInformation => _customInformation = _customInformation ?? new Dictionary<string, object>();

        private static Expression<Func<T, TAssignmentProperty, TAssignmentProperty>> GetAssignmentLambda<T, TAssignmentProperty>(string name)
        {
            var p = Expression.Parameter(typeof(T), "o");
            var v = Expression.Parameter(typeof(T).GetProperty(name).PropertyType, "v");
            var l = Expression.Lambda(Expression.Assign(Expression.Property(p, name), v), p, v);
            return (Expression<Func<T, TAssignmentProperty, TAssignmentProperty>>)l;
        }
    }

    public interface IEntityProperty<T> : IProperty
        where T : class
    {
        new MediaKey<T> MediaKey { get; }
    }
}