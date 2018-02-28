using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Iql.Extensions;
using Iql.Queryable.Data.EntityConfiguration.DisplayFormatting;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.EntityConfiguration.Validation;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Extensions;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfiguration<T> : EntityConfigurationBase, IEntityConfiguration where T : class
    {
        private readonly EntityConfigurationBuilder _builder;
        private readonly Dictionary<string, IProperty> _propertiesMap = new Dictionary<string, IProperty>();

        public ValidationCollection<T> EntityValidation { get; } = new ValidationCollection<T>();

        public DisplayFormatting<T> DisplayFormatting { get; }

        IDisplayFormatting IEntityConfiguration.DisplayFormatting => DisplayFormatting;
        IValidationCollection IEntityConfiguration.EntityValidation => EntityValidation;

        public EntityConfiguration(Type type, EntityConfigurationBuilder builder)
        {
            Type = type;
            _builder = builder;
            DisplayFormatting = new DisplayFormatting<T>(this);
            Relationships = new List<IRelationship>();
            Properties = new List<IProperty>();
        }

        public string GetDisplayText(T entity, string key = null)
        {
            return DisplayFormatting.TryFormat(entity, key);
        }

        string IEntityConfiguration.GetDisplayText(object entity, string key = null)
        {
            return GetDisplayText((T) entity, key);
        }

        IEntityValidationResult IEntityConfiguration.ValidateEntity(object entity)
        {
            return ValidateEntity((T) entity);
        }

        IPropertyValidationResult IEntityConfiguration.ValidateEntityPropertyByExpression<TProperty>(object entity,
            Expression<Func<object, TProperty>> expression)
        {
            var property = ((IEntityConfiguration) this).FindPropertyByExpression(expression);
            return ValidateEntityProperty((T)entity, property);
        }

        IPropertyValidationResult IEntityConfiguration.ValidateEntityPropertyByName(object entity, string property)
        {
            return ValidateEntityPropertyByName((T) entity, property);
        }

        IPropertyValidationResult IEntityConfiguration.ValidateEntityProperty(object entity, IProperty property)
        {
            return ValidateEntityProperty((T) entity, property);
        }

        public EntityValidationResult<T> ValidateEntity(T entity)
        {
            var validationResult = new EntityValidationResult<T>(entity);
            foreach (var validation in EntityValidation.All)
            {
                if (!validation.Validate(entity))
                {
                    validationResult.AddFailure(validation.Key, validation.Message);
                }
            }

            foreach (var property in Properties)
            {
                var result = ValidateEntityProperty(entity, property);
                if (result.HasValidationFailures())
                {
                    validationResult.AddPropertyValidationResult(result);
                }
            }
            return validationResult;
        }

        public PropertyValidationResult<T> ValidateEntityPropertyByExpression<TProperty>(T entity, Expression<Func<T, TProperty>> property)
        {
            return ValidateEntityProperty(entity, FindPropertyByExpression(property));
        }

        public PropertyValidationResult<T> ValidateEntityPropertyByName(T entity, string property)
        {
            return ValidateEntityProperty(entity, FindProperty(property));
        }

        public PropertyValidationResult<T> ValidateEntityProperty(T entity, IProperty property)
        {
            var validationResult = new PropertyValidationResult<T>(entity, property);
            foreach (var validation in property.Validation.All)
            {
                if (!validation.Validate(entity))
                {
                    validationResult.AddFailure(validation.Key, validation.Message);
                }
            }
            return validationResult;
        }

        public List<IRelationship> Relationships { get; set; }

        private IProperty FindOrDefinePropertyInternal(
            LambdaExpression lambda, 
            Type propertyType, 
            Type elementType)
        {
            return (IProperty)GetType()
                .GetRuntimeMethods()
                .First(m => m.Name == nameof(FindOrDefineProperty))
                .InvokeGeneric(this, new object[]
                {
                    lambda, elementType
                }, propertyType);
        }

        public IProperty FindOrDefineProperty<TProperty>(LambdaExpression lambda, Type elementType)
        {
            var expression = (Expression<Func<T, TProperty>>) lambda;
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(expression) as
                IqlPropertyExpression;
            var property = FindProperty(iql.PropertyName);
            if (property == null)
            {
                if (TypeExtensions.IsEnumerable<TProperty>())
                {
                    InvokeDefineCollectionPropertyInternal<TProperty>(
                        typeof(T).GetProperty(iql.PropertyName).PropertyType, elementType, lambda, null);
                }
                else
                {
                    DefineProperty(expression);
                }
                property = FindProperty(iql.PropertyName);
            }
            return property;
        }

        public IProperty FindOrDefinePropertyByName(string name, Type elementType)
        {
            var property = typeof(T).GetProperty(name);
            return FindOrDefinePropertyInternal(GetLambdaExpression<T>(property.Name), property.PropertyType ?? elementType, property.PropertyType ?? elementType);
        }

        public RelationshipMatch FindRelationship(string propertyName)
        {
            return AllRelationships().SingleOrDefault(r => r.ThisEnd.Property.Name == propertyName);
        }

        public List<RelationshipMatch> AllRelationships()
        {
            return AllRelationshipsInternal(true);
        }

        public List<RelationshipMatch> AllRelationshipsInternal(bool nested)
        {
            var list = new List<RelationshipMatch>();
            foreach (var relationship in Relationships)
            {
                var ends = new[] { relationship.Source, relationship.Target };
                for (var i = 0; i < ends.Length; i++)
                {
                    if (ends[i].Configuration == this)
                    {
                        var relationshipMatch = new RelationshipMatch(relationship, i == 1);
                        if (relationship.Kind == RelationshipKind.OneToOne && nested)
                        {
                            //var matches = (List<RelationshipMatch>)GetType().GetMethod(nameof(FindAllRelationships),
                            //    BindingFlags.Instance | BindingFlags.NonPublic)
                            //    .MakeGenericMethod(relationshipMatch.OtherEnd.Type)
                            //    .Invoke(this, new object[]{ relationshipMatch.OtherEnd.Configuration });
                            //var match = matches.SingleOrDefault(m => m.Relationship.Type == RelationshipType.OneToOne &&
                            //                                         m.OtherEnd.Property.PropertyName ==
                            //                                         relationshipMatch.ThisEnd.Property.PropertyName &&
                            //                                         m.ThisEnd.Property.PropertyName ==
                            //                                         relationshipMatch.OtherEnd.Property.PropertyName &&
                            //                                         relationshipMatch.Relationship != relationship);
                            //relationshipMatch.InverseOneToOneRelationship = match;
                            //match.InverseOneToOneRelationship = relationshipMatch;
                        }
                        list.Add(relationshipMatch);
                    }
                }
            }
            return list;
        }

        private List<RelationshipMatch> FindAllRelationships<TRelationship>(EntityConfiguration<TRelationship> configuration) where TRelationship : class
        {
            return configuration.AllRelationshipsInternal(false);
        }

        public bool EntityHasKey(object entity, CompositeKey key)
        {
            var isMatch = true;
            foreach (var id in Key.Properties)
            {
                var compositeKeyValue = key.Keys.SingleOrDefault(k => k.Name == id.Name);
                if (compositeKeyValue == null)
                {
                    return false;
                }
                if (!Equals(entity.GetPropertyValue(id), compositeKeyValue.Value))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }

        public bool KeysMatch(object left, object right)
        {
            if (new[] { left, right }.Count(i => i == null) == 1)
            {
                return false;
            }
            if (left.GetType() != right.GetType())
            {
                return false;
            }
            if (left == right)
            {
                return true;
            }
            var isMatch = true;
            foreach (var id in Key.Properties)
            {
                if (!Equals(left.GetPropertyValue(id), right.GetPropertyValue(id)))
                {
                    isMatch = false;
                    break;
                }
            }
            return isMatch;
        }

        public CompositeKey GetCompositeKey(object entity)
        {
            var key = new CompositeKey(Key.Properties.Count);
            for (var i = 0; i < Key.Properties.Count; i++)
            {
                var property = Key.Properties[i];
                key.Keys[i] = new KeyValue(property.Name, entity.GetPropertyValue(property), property.ElementType);
            }
            return key;
        }

        public List<IProperty> Properties { get; set; }
        public IEntityKey Key { get; set; }
        public Type Type { get; set; }

        public IProperty FindProperty(string name)
        {
            return _propertiesMap.ContainsKey(name) ? _propertiesMap[name] : null;
        }

        public IProperty FindPropertyByExpression<TProperty>(Expression<Func<T, TProperty>> property)
        {
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            return FindProperty(iql.PropertyName);
        }

        IProperty IEntityConfiguration.FindPropertyByExpression<TProperty>(
            Expression<Func<object, TProperty>> expression)
        {
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(expression) as IqlPropertyExpression;
            return FindProperty(iql.PropertyName);
        }

        public IEntityKey GetKey()
        {
            return Key;
        }

        public EntityConfiguration<T> PrimaryKeyIsGeneratedRemotely(
            bool isTrue = true
        )
        {
            Key.IsGeneratedRemotely = isTrue;
            return this;
        }

        public EntityConfiguration<T> HasKey<TKey>(
            Expression<Func<T, TKey>> property
        )
        {
            DefineProperty(property);
            Key = new EntityKey<T, TKey>();
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            iql.ReturnType = typeof(TKey).ToIqlType();
            Key.Properties.Add(FindOrDefineProperty<TKey>(property, typeof(TKey)));
            TrySetKey(iql.PropertyName);
            return this;
        }

        private void TrySetKey(string propertyName)
        {
            if (Key != null && Key.Properties.Any(p => p.Name == propertyName))
            {
                var property = FindProperty(propertyName);
                if (property != null)
                {
                    property.Kind = PropertyKind.Key;
                    property.Relationship = null;
                }
            }
        }

        public EntityConfiguration<T> HasCompositeKey(
            params Expression<Func<T, object>>[] properties
        )
        {
            Key = new EntityKey<T, CompositeKey>();
            foreach (var property in properties)
            {
                var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
                var propertyType = typeof(T).GetProperty(iql.PropertyName).PropertyType;
                //iql.ReturnType = typeof(T).getpro.ToIqlType();
                Key.Properties.Add(FindOrDefinePropertyInternal(GetLambdaExpression<T>(iql.PropertyName), propertyType, propertyType));
            }
            return this;
        }

        public EntityConfiguration<T> DefineConvertedProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            string convertedFromType,
            bool nullable = true
        )
        {
#if !TypeScript
            if (TypeExtensions.IsEnumerable<TProperty>())
            {
                throw new Exception($"Please use {nameof(DefineCollectionProperty)} to define collection properties.");
            }
#endif
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<T, TProperty, TProperty> 
                             ?? new Property<T, TProperty, TProperty>(
                                 name, 
                                 false, 
                                 typeof(T), 
                                 convertedFromType, 
                                 false, 
                                 null, 
                                 property);
            definition.Nullable = nullable;

            //definition.PropertyGetter = property.Compile();
            //definition.PropertyGetterExpression = property;
            if (!Properties.Contains(definition))
            {
                Properties.Add(definition);
                _propertiesMap[name] = definition;
            }
            var relationship = FindRelationship(name);
            if (relationship != null)
            {
                definition.Kind = PropertyKind.Relationship;
                definition.Relationship = relationship;
            }
            TrySetKey(iql.PropertyName);
            return this;
        }

        public EntityConfiguration<T> DefineDisplayFormatter(Expression<Func<T, string>> formatter,
            string key = null)
        {
            DisplayFormatting.Set(formatter, key);
            return this;
        }

        public EntityConfiguration<T> DefineEntityValidation(Expression<Func<T, bool>> validation,
            string key,
            string message)
        {
            EntityValidation.Add(validation, key, message);
            return this;
        }

        public EntityConfiguration<T> DefinePropertyValidation<TProperty>(
            Expression<Func<T, TProperty>> property,
            Expression<Func<T, bool>> validation,
            string key,
            string message)
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(property, typeof(TProperty));
            var validationCollection = (ValidationCollection<T>)propertyDefinition.Validation;
            validationCollection.Add(validation, key, message);
            return this;
        }

        public EntityConfiguration<T> DefineProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            bool nullable = true
        )
        {
            return DefineConvertedProperty(property, null, nullable);
        }

        public EntityConfiguration<T> DefineCollectionProperty<TProperty>(
            Expression<Func<T, IEnumerable<TProperty>>> property,
            Expression<Func<T, long?>> countProperty = null
            )
        {
            var iql =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            var propertyInfo = typeof(T).GetProperty(iql.PropertyName);
            var propertyRuntimeType = propertyInfo.PropertyType;
            var propertyName = iql.PropertyName;
#if TypeScript
            propertyRuntimeType = typeof(TProperty);
#endif
            var lambda = GetLambdaExpression<T>(propertyName);
            InvokeDefineCollectionPropertyInternal<TProperty>(propertyRuntimeType, typeof(TProperty), lambda, countProperty);
            return this;
        }

        private void InvokeDefineCollectionPropertyInternal<TProperty>(
            Type propertyRuntimeType,
            Type elementType,
            LambdaExpression lambda,
            Expression<Func<T, long?>> countProperty = null)
        {
            var propertyType = typeof(TProperty);
#if !TypeScript
            if (TypeExtensions.IsEnumerableType(propertyType))
            {
                while (propertyType.GenericTypeArguments.Length > 1)
                {
                    propertyType = propertyType.BaseType;
                }
                propertyType = propertyType.GenericTypeArguments[0];
            }
#endif
            GetType()
                .GetRuntimeMethods()
                .First(m => m.Name == nameof(DefineCollectionPropertyInternal))
                .InvokeGeneric(this,
                    new object[]
                    {
                        lambda, countProperty
                    },
                    propertyRuntimeType, propertyType)
                ;
        }

        private EntityConfiguration<T> DefineCollectionPropertyInternal<TValueType, TElementType>(
            Expression<Func<T, TValueType>> property,
            Expression<Func<T, long?>> countProperty = null
        )
        {
            var collection = MapProperty<TElementType, TValueType>(property, true, false, null);
            if (countProperty != null)
            {
                var countPropertyIql =
                    IqlQueryableAdapter.ExpressionToIqlExpressionTree(countProperty) as IqlPropertyExpression;
                var countPropertyDefinition = typeof(T).GetProperty(countPropertyIql.PropertyName);
                if (Nullable.GetUnderlyingType(countPropertyDefinition.PropertyType) != null)
                {
                    var countDefinition = MapProperty<long?, long?>(countProperty, false, true, collection);
                    countDefinition.Kind = PropertyKind.Count;
                    countDefinition.Nullable = true;
                }
                else
                {
                    var lambdaExpression = GetLambdaExpression<T>(countPropertyIql.PropertyName);
                    if (countPropertyDefinition.PropertyType == typeof(Int32))
                    {
                        var lambda = (Expression<Func<T, int>>) lambdaExpression;
                        var countDefinition = MapProperty<int, int>(lambda, false, true, collection);
                        countDefinition.Kind = PropertyKind.Count;
#if TypeScript
                        countDefinition.Nullable = true;
#endif
                    }
                    else
                    {
                        var lambda = (Expression<Func<T, long>>)lambdaExpression;
                        var countDefinition = MapProperty<long, long>(lambda, false, true, collection);
                        countDefinition.Kind = PropertyKind.Count;
#if TypeScript
                        countDefinition.Nullable = true;
#endif
                    }
                }
            }
            TryAssignRelationshipToPropertyDefinition(collection);
            return this;
        }

        private static LambdaExpression GetLambdaExpression<TOwner>(string propertyName)
        {
            var param = Expression.Parameter(typeof(TOwner), "o");
            var lambda = Expression.Lambda(Expression.Property(param, propertyName), param);
            return lambda;
        }

        private Property<T, TPropertyType, TElementType> MapProperty<TElementType, TPropertyType>(
            Expression<Func<T, TPropertyType>> property,
            bool isCollection,
            bool readOnly,
            IProperty countRelationship)
        {
            var iql =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(property) as IqlPropertyExpression;
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<T, TPropertyType, TElementType>;
            if (definition == null)
            {
                definition = new Property<T, TPropertyType, TElementType>(
                    name, 
                    isCollection, 
                    typeof(T), 
                    null, 
                    readOnly,
                    countRelationship, 
                    property);
            }
            else
            {
                definition.ConfigureProperty(
                    name, 
                    isCollection, 
                    typeof(T),
                    typeof(TPropertyType),
                    typeof(TElementType), 
                    null,
                    readOnly,
                    countRelationship, 
                    property);
            }
            if (!Properties.Contains(definition))
            {
                Properties.Add(definition);
                _propertiesMap[name] = definition;
            }
            TryAssignRelationshipToPropertyDefinition(definition);
            return definition;
        }

        public OneToRelationshipMap<T, TTarget> HasOne<TTarget>(
            Expression<Func<T, TTarget>> property) where TTarget : class
        {
            return new OneToRelationshipMap<T, TTarget>(
                _builder,
                this,
                _builder.EntityType<TTarget>(),
                RelationshipMapType.One,
                property);
        }

        public ManyToRelationshipMap<T, TTarget> HasMany<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> property) where TTarget : class
        {
            return new ManyToRelationshipMap<T, TTarget>(
                _builder,
                this,
                _builder.EntityType<TTarget>(),
                RelationshipMapType.Many,
                property);
        }

        internal override void TryAssignRelationshipToProperty(string propertyName, bool tryAssignOtherEnd = true)
        {
            var propertyDefinition = FindProperty(propertyName);
            if (propertyDefinition != null)
            {
                TryAssignRelationshipToPropertyDefinition(propertyDefinition, tryAssignOtherEnd);
            }
        }

        internal override void TryAssignRelationshipToPropertyDefinition(IProperty definition, bool tryAssignOtherEnd = true)
        {
            var relationship = FindRelationship(definition.Name);
            if (relationship != null)
            {
                definition.Kind = PropertyKind.Relationship;
                definition.Relationship = relationship;
                var otherEndConfiguration = _builder.GetEntityByType(relationship.OtherEnd.Type);
                foreach (var constraint in relationship.Relationship.Constraints)
                {
                    var constraintProperty = otherEndConfiguration.FindProperty(constraint.SourceKeyProperty.Name);
                    if (constraintProperty != null && constraintProperty.Kind != PropertyKind.RelationshipKey && constraintProperty.Kind != PropertyKind.Key)
                    {
                        constraintProperty.Kind = PropertyKind.RelationshipKey;
                        constraintProperty.Relationship = otherEndConfiguration.FindRelationship(relationship.OtherEnd.Property.Name);
                    }
                }
                if (tryAssignOtherEnd)
                {
                    (otherEndConfiguration as EntityConfigurationBase)
                        .TryAssignRelationshipToProperty(relationship.OtherEnd.Property.Name, false);
                }
            }
            else
            {
                foreach (var relationshipMatch in AllRelationships())
                {
                    foreach (var constraint in relationshipMatch.Relationship.Constraints)
                    {
                        if (constraint.SourceKeyProperty.Name == definition.Name && definition.Kind != PropertyKind.RelationshipKey && definition.Kind != PropertyKind.Key)
                        {
                            definition.Kind = PropertyKind.RelationshipKey;
                            definition.Relationship = relationshipMatch;
                        }
                    }
                }
            }
        }
    }
}