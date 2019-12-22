using Iql.Entities.Rules;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Validation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities.InferredValues;
using Iql.Entities.Permissions;
using Iql.Entities.ValueResolvers;
using Iql.Extensions;

namespace Iql.Entities
{
    [DebuggerDisplay("{Name} - {Kind}")]
    public class Property<TOwner, TProperty, TElementType> : PropertyBase, IEntityProperty<TOwner>
        where TOwner : class
    {
        private Dictionary<string, object> _customInformation;
        private Expression<Func<TOwner, TProperty, TProperty>> _propertySetterExpressionTyped;
        private Func<TOwner, TProperty, TProperty> _propertySetterTyped;

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

        public IEntityProperty<TOwner> DefineUserPermission<TUser>(
            Expression<Func<IqlEntityUserPermissionContext<TOwner, TUser>, IqlUserPermission>> rule)
            where TUser : class
        {
            return this;
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
            EntityConfigurationInternal = entityConfiguration;
            ((MetadataBase) this).Name = name;
            Name = name;
            if (readOnly.HasValue)
            {
                SetEditorReadOnly();
            }
            CountRelationshipProperty = countRelationship;
            if (countRelationship != null)
            {
                ((PropertyBase)countRelationship).CountRelationshipProperty = this;
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

            SetValue = (o, v) =>
            {
                if (EntityConfigurationInternal.SpecialTypeDefinition != null)
                {
                    var entityType = o.GetType();
                    if (EntityConfiguration.Type != entityType)
                    {
                        if (entityType == EntityConfigurationInternal.SpecialTypeDefinition.InternalType)
                        {
                            return EntityConfigurationInternal.SpecialTypeDefinition.ResolvePropertyMapInverse(Name)
                                .CustomProperty.SetValue(o, v);
                        }

                        if (entityType == EntityConfigurationInternal.SpecialTypeDefinition.EntityConfiguration.Type)
                        {
                            return EntityConfigurationInternal.SpecialTypeDefinition.ResolvePropertyMap(Name)
                                .CustomProperty.SetValue(o, v);
                        }
                    }
                }

                return PropertySetterTyped((TOwner)o, (TProperty)TypeDefinition.EnsureValueType(v));
            };
        }

        public new ValidationCollection<TOwner> ValidationRules => (ValidationCollection<TOwner>)base.ValidationRules;

        public override ReadOnlyEditDisplayKind ResolvedReadOnlyEditDisplayKind
        {
            get
            {
                if (Relationship != null)
                {
                    return Relationship.ThisEnd.ReadOnlyEditDisplayKind;
                }

                return ReadOnlyEditDisplayKind;
            }
        }

        IRuleCollection<IBinaryRule> IPropertyGroup.ValidationRules
        {
            get => ValidationRules;
            set => throw new NotImplementedException();
        }

        protected override IRuleCollection<IDisplayRule> NewDisplayRulesCollection()
        {
            return new DisplayRuleCollection<TOwner>();
        }

        protected override IRuleCollection<IBinaryRule> NewValidationRulesCollection()
        {
            return new ValidationCollection<TOwner>();
        }

        protected override IRuleCollection<IRelationshipRule> NewRelationshipFilterRulesCollection()
        {
            return new RelationshipRuleCollection<TOwner, TElementType>();
        }

        public new DisplayRuleCollection<TOwner> DisplayRules => (DisplayRuleCollection<TOwner>)base.DisplayRules;

        IRuleCollection<IDisplayRule> IPropertyGroup.DisplayRules
        {
            get => DisplayRules;
            set => throw new NotImplementedException();
        }

        public override IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.Primitive;
        public override PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[]{};
        }

        public new RelationshipRuleCollection<TOwner, TElementType> RelationshipFilterRules => (RelationshipRuleCollection<TOwner, TElementType>)base.RelationshipFilterRules;

        IRuleCollection<IRelationshipRule> IPropertyGroup.RelationshipFilterRules
        {
            get => RelationshipFilterRules;
            set => throw new NotImplementedException();
        }

        public Expression<Func<TOwner, TProperty>> PropertyGetterExpressionTyped { get; set; }

        public Expression<Func<TOwner, TProperty, TProperty>> PropertySetterExpressionTyped =>
            _propertySetterExpressionTyped = _propertySetterExpressionTyped ?? GetAssignmentLambda<TOwner, TProperty>(Name);

        public Func<TOwner, TProperty> PropertyGetterTyped { get; set; }

        public Func<TOwner, TProperty, TProperty> PropertySetterTyped =>
            _propertySetterTyped = _propertySetterTyped ?? PropertySetterExpressionTyped.Compile();

        public override Func<object, object> GetValue { get; set; }
        public override Func<object, object, object> SetValue { get; set; }

        public Dictionary<string, object> CustomInformation => _customInformation = _customInformation ?? new Dictionary<string, object>();

        public IValueResolver<TOwner> DefaultValueResolver { get; set; }

        public Property<TOwner, TProperty, TElementType> UseLiteralDefaultValue(TElementType value)
        {
            DefaultValueResolver = new LiteralValueResolver<TOwner>(value);
            return this;
        }

        IEntityProperty<TOwner> IEntityProperty<TOwner>.UseLiteralDefaultValue(object value)
        {
            return UseLiteralDefaultValue((TElementType)value);
        }

        public Property<TOwner, TProperty, TElementType> UseFunctionDefaultValue(Func<TOwner, Task<TElementType>> resolver)
        {
            DefaultValueResolver = new FunctionValueResolver<TOwner>(async owner => await resolver(owner));
            return this;
        }

        IEntityProperty<TOwner> IEntityProperty<TOwner>.UseFunctionDefaultValue(Func<TOwner, Task<object>> resolver)
        {
            return UseFunctionDefaultValue(async owner => (TElementType)await resolver(owner));
        }

        public Property<TOwner, TProperty, TElementType> IsInferredWith(Expression<Func<InferredValueContext<TOwner>, object>> expression, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false, params string[] onPropertyChanges)
        {
            SetInferredWithExpression(expression, onlyIfNew, kind, canOverride, onPropertyChanges);
            return this;
        }

        public Property<TOwner, TProperty, TElementType> IsConditionallyInferredWith(
            Expression<Func<InferredValueContext<TOwner>, object>> expression, Expression<Func<InferredValueContext<TOwner>, bool>> condition)
        {
            SetConditionallyInferredWithExpression(expression, condition);
            return this;
        }

        public IEntityProperty<TOwner> Configure(Action<IEntityProperty<TOwner>> configure)
        {
            if (configure != null)
            {
                configure(this);
            }
            return this;
        }

        IEntityProperty<TOwner> IEntityProperty<TOwner>.IsInferredWith(Expression<Func<InferredValueContext<TOwner>, object>> expression, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false, params string[] onPropertyChanges)
        {
            return IsInferredWith(expression, onlyIfNew, kind, canOverride, onPropertyChanges);
        }

        IEntityProperty<TOwner> IEntityProperty<TOwner>.IsConditionallyInferredWith(Expression<Func<InferredValueContext<TOwner>, object>> expression, Expression<Func<InferredValueContext<TOwner>, bool>> condition)
        {
            return IsConditionallyInferredWith(expression, condition);
        }

        IProperty IProperty.IsInferredWithExpression(LambdaExpression expression, bool onlyIfNew = false, InferredValueKind kind = InferredValueKind.Always, bool canOverride = false)
        {
            SetInferredWithExpression(expression, onlyIfNew, kind, canOverride);
            return this;
        }

        private static Expression<Func<T, TAssignmentProperty, TAssignmentProperty>> GetAssignmentLambda<T, TAssignmentProperty>(string name)
        {
#if TypeScript
            Expression<Func<T, TAssignmentProperty, TAssignmentProperty>> lambda = (entity, value) =>
                (TAssignmentProperty)entity.SetPropertyValueByName(name, value);
            return lambda;
#else
            var p = Expression.Parameter(typeof(T), "o");
            var v = Expression.Parameter(typeof(T).GetProperty(name).PropertyType, "v");
            var l = Expression.Lambda(Expression.Assign(Expression.Property(p, name), v), p, v);
            return (Expression<Func<T, TAssignmentProperty, TAssignmentProperty>>)l;
#endif
        }
   }
}