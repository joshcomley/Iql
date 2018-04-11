using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Iql.Extensions;
using Iql.Queryable.Data.DataStores.InMemory.QueryApplicator;
using Iql.Queryable.Data.EntityConfiguration.DisplayFormatting;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.EntityConfiguration.Validation;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Extensions;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityConfiguration<T> : EntityConfigurationBase, IEntityConfiguration where T : class
    {
        class DefaultValuePlaceholder { }

        private static readonly DefaultValuePlaceholder DefaultValuePlaceholderInstance = new DefaultValuePlaceholder();

        public bool HasNonKeyFields()
        {
            for (var i = 0; i < Properties.Count; i++)
            {
                var property = Properties[i];
                if (property.Kind.HasFlag(PropertyKind.Primitive) &&
                    !property.Kind.HasFlag(PropertyKind.Key) &&
                    !property.Kind.HasFlag(PropertyKind.Count))
                {
                    return true;
                }

                if (property.Kind.HasFlag(PropertyKind.Relationship))
                {
                    var constraints = property.Relationship.ThisEnd.Constraints();
                    if (constraints.Any(c => !c.Kind.HasFlag(PropertyKind.Key)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasRelationshipKeys => Key != null && Key.HasRelationshipKeys;

        public EntityConfigurationBuilder Builder { get; }
        private readonly Dictionary<string, IProperty> _propertiesMap = new Dictionary<string, IProperty>();

        public ValidationCollection<T> EntityValidation { get; } = new ValidationCollection<T>();

        public DisplayFormatting<T> DisplayFormatting { get; }

        IDisplayFormatting IEntityConfiguration.DisplayFormatting => DisplayFormatting;
        IValidationCollection IEntityConfiguration.EntityValidation => EntityValidation;

        public EntityConfiguration(Type type, EntityConfigurationBuilder builder)
        {
            Type = type;
            Builder = builder;
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
            return GetDisplayText((T)entity, key);
        }

        public IProperty[] ResolveSearchProperties(PropertySearchKind searchKind = PropertySearchKind.Primary)
        {
            var result = new List<IProperty>();
            foreach (var property in Properties)
            {
                if (property.SearchKind != PropertySearchKind.None && property.SearchKind == searchKind)
                {
                    result.Add(property);
                }
            }

            if (!result.Any() && searchKind == PropertySearchKind.Primary)
            {
                var secondarySearchFields = ResolveSearchProperties(PropertySearchKind.Secondary);
                if (secondarySearchFields.Any())
                {
                    var priorityFields = new[] { "FullName", "Name", "Title" };
                    foreach (var priorityField in priorityFields)
                    {
                        var priorityFieldMatch = secondarySearchFields.FirstOrDefault(f => f.Name == priorityField);
                        if (priorityFieldMatch != null)
                        {
                            result.Add(priorityFieldMatch);
                            return result.ToArray();
                        }
                    }

                    result.Add(secondarySearchFields.First());
                }
            }

            return result.ToArray();
        }

        IEntityValidationResult IEntityConfiguration.ValidateEntity(object entity)
        {
            return ValidateEntity((T)entity);
        }

        IPropertyValidationResult IEntityConfiguration.ValidateEntityPropertyByExpression<TProperty>(object entity,
            Expression<Func<object, TProperty>> expression)
        {
            var property = ((IEntityConfiguration)this).FindPropertyByLambdaExpression(expression);
            return ValidateEntityProperty((T)entity, property);
        }

        IPropertyValidationResult IEntityConfiguration.ValidateEntityPropertyByName(object entity, string property)
        {
            return ValidateEntityPropertyByName((T)entity, property);
        }

        IPropertyValidationResult IEntityConfiguration.ValidateEntityProperty(object entity, IProperty property)
        {
            return ValidateEntityProperty((T)entity, property);
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
            return ValidateEntityProperty(entity, FindPropertyByLambdaExpression(property));
        }

        public PropertyValidationResult<T> ValidateEntityPropertyByName(T entity, string property)
        {
            return ValidateEntityProperty(entity, FindProperty(property));
        }

        public PropertyValidationResult<T> ValidateEntityProperty(T entity, IProperty property)
        {
            return ValidateEntityPropertyInternal(entity, property, false);
        }

        public PropertyValidationResult<T> ValidateEntityPropertyInternal(T entity, IProperty property, bool hasSetDefaultValue)
        {
            var validationResult = new PropertyValidationResult<T>(entity, property);
            foreach (var validation in property.Validation.All)
            {
                if (!validation.Validate(entity))
                {
                    validationResult.AddFailure(validation.Key, validation.Message);
                }
            }

            if (!property.Kind.HasFlag(PropertyKind.Count) && !property.Kind.HasFlag(PropertyKind.Key))
            {
                var propertyValue = property.PropertyGetter(entity);
                if (!validationResult.HasValidationFailures() &&
                PropertyValueIsEmpty(property, entity, propertyValue)
            )
                {
                    if (!hasSetDefaultValue)
                    {
                        // Mimic default values for 
                        object newValue = DefaultValuePlaceholderInstance;
                        if (property.TypeDefinition.ConvertedFromType == nameof(Guid) ||
                            property.TypeDefinition.Kind == IqlType.Date ||
                            property.TypeDefinition.Kind == IqlType.Enum)
                        {
                            newValue = property.TypeDefinition.DefaultValue();
                        }
                        if (!Equals(newValue, DefaultValuePlaceholderInstance))
                        {
                            property.PropertySetter(entity, newValue);
                            return ValidateEntityPropertyInternal(entity, property, true);
                        }
                    }
                    validationResult.AddFailure(
                        DefaultRequiredAutoValidationFailureKey,
                        DefaultRequiredAutoValidationFailureMessage);
                }
            }
            return validationResult;
        }

        private static bool PropertyValueIsEmpty(IProperty property, object entity, object propertyValue)
        {
            if (property.ReadOnly)
            {
                return false;
            }

            if (property.TypeDefinition.Nullable)
            {
                return false;
            }

            if (property.Kind.HasFlag(PropertyKind.Relationship) &&
                propertyValue == null)
            {
                if (!property.Relationship.ThisIsTarget)
                {
                    var properties = property.Relationship.ThisEnd.Constraints();
                    for (var i = 0; i < properties.Length; i++)
                    {
                        var constraint = properties[i];
                        var constraintValue = constraint.PropertyGetter(entity);
                        if (Equals(null, constraintValue) ||
                            Equals(constraint.TypeDefinition.DefaultValue(), constraintValue))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (Equals(null, propertyValue))
            {
                return true;
            }

            if (property.TypeDefinition.Kind == IqlType.Enum)
            {
                var stringValue = Enum.ToObject(property.TypeDefinition.Type, propertyValue).ToString();
                if (string.IsNullOrWhiteSpace(stringValue) || Regex.IsMatch(stringValue, @"^\d+$"))
                {
                    return true;
                }
            }

            if (property.TypeDefinition.Kind == IqlType.Date)
            {
                if (propertyValue.IsDefaultValue(property.TypeDefinition))
                {
                    return true;
                }
            }

            if (property.TypeDefinition.Type == typeof(string) && Equals(propertyValue, ""))
            {
                return true;
            }

            return false;
        }

        public List<IRelationship> Relationships { get; set; }

        private IProperty FindOrDefinePropertyInternal(
            LambdaExpression lambda,
            Type propertyType,
            Type elementType,
            IqlType? iqlType)
        {
            return (IProperty)GetType()
                .GetRuntimeMethods()
                .First(m => m.Name == nameof(FindOrDefineProperty))
                .InvokeGeneric(this, new object[]
                {
                    lambda,
                    elementType,
                    iqlType
                }, propertyType);
        }

        public IProperty FindOrDefineProperty<TProperty>(LambdaExpression lambda, Type elementType, IqlType? iqlType)
        {
            var expression = (Expression<Func<T, TProperty>>)lambda;
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(expression);
            var property = FindProperty(iql.PropertyName);
            if (property == null)
            {
                if (typeof(TProperty).IsEnumerableType())
                {
                    InvokeDefineCollectionPropertyInternal<TProperty>(
                        typeof(T).GetProperty(iql.PropertyName).PropertyType, elementType, lambda, null);
                }
                else
                {
                    DefineProperty(expression, true, iqlType);
                }
                property = FindProperty(iql.PropertyName);
            }
            return property;
        }

        public IProperty FindOrDefinePropertyByName(string name, Type elementType)
        {
            var property = typeof(T).GetProperty(name);
            return FindOrDefinePropertyInternal(GetLambdaExpression<T>(property.Name), property.PropertyType ?? elementType, property.PropertyType ?? elementType, null);
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
            var key = new CompositeKey(Key.Properties.Length);
            for (var i = 0; i < Key.Properties.Length; i++)
            {
                var property = Key.Properties[i];
                key.Keys[i] = new KeyValue(property.Name, entity.GetPropertyValue(property), property.TypeDefinition);
            }
            return key;
        }

        public IProperty FindProperty(string name)
        {
            return _propertiesMap.ContainsKey(name) ? _propertiesMap[name] : null;
        }

        public IProperty FindPropertyByLambdaExpression(LambdaExpression property)
        {
            var iql = IqlQueryableAdapter.LambdaExpressionToIqlExpressionTree(property, typeof(T));
            return FindProperty(iql.PropertyName);
        }

        public IProperty FindPropertyByExpression(Expression<Func<T, object>> property)
        {
            return FindPropertyByLambdaExpression(property);
        }

        IProperty IEntityConfiguration.FindPropertyByExpression(
            Expression<Func<object, object>> expression)
        {
            return FindPropertyByLambdaExpression(expression);
        }
        
        public EntityConfiguration<T> HasKey<TKey>(
            Expression<Func<T, TKey>> property,
            IqlType? iqlType = null
        )
        {
            var definedProperty = DefineAndGetProperty(property, null, false, iqlType);
            Key = new EntityKey<T, TKey>();
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property);
            iql.ReturnType = iqlType ?? typeof(TKey).ToIqlType();
            Key.AddProperty(definedProperty);
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
                    property.Kind = property.Kind | PropertyKind.Key;
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
                var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property);
                var propertyType = typeof(T).GetProperty(iql.PropertyName).PropertyType;
                //iql.ReturnType = typeof(T).getpro.ToIqlType();
                Key.AddProperty(FindOrDefinePropertyInternal(GetLambdaExpression<T>(iql.PropertyName), propertyType, propertyType, null));
            }
            return this;
        }

        public EntityConfiguration<T> Configure(
            Action<EntityConfiguration<T>> configure)
        {
            configure(this);
            return this;
        }

        public EntityConfiguration<T> ConfigureProperty(
            Expression<Func<T, object>> property,
            Action<IProperty> configure)
        {
            var propertyDefinition = FindPropertyByExpression(property);
            configure(propertyDefinition);
            return this;
        }

        public EntityConfiguration<T> DefineConvertedProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            string convertedFromType,
            bool nullable = true,
            IqlType? iqlType = null
        )
        {
            DefineAndGetProperty(property, convertedFromType, nullable, iqlType);
            return this;
        }

        public IProperty DefineAndGetProperty<TProperty>(Expression<Func<T, TProperty>> property, string convertedFromType, bool nullable = true,
            IqlType? iqlType = null)
        {
#if !TypeScript
            if (typeof(TProperty).IsEnumerableType())
            {
                throw new Exception($"Please use {nameof(DefineCollectionProperty)} to define collection properties.");
            }
#endif
            var iql = IqlQueryableAdapter.ExpressionToIqlExpressionTree(property);
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<T, TProperty, TProperty>
                             ?? new Property<T, TProperty, TProperty>(
                                 name,
                                 nullable,
                                 false,
                                 typeof(T),
                                 convertedFromType,
                                 null,
                                 iqlType ?? typeof(TProperty).ToIqlType(),
                                 null,
                                 property);

            definition.TypeDefinition = definition.TypeDefinition.ChangeType(typeof(TProperty));
            definition.TypeDefinition = definition.TypeDefinition.ChangeNullable(nullable);
            if (iqlType != null && iqlType != IqlType.Unknown)
            {
                definition.TypeDefinition = definition.TypeDefinition.ChangeKind(iqlType.Value);
            }

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

            if (definition.Kind == 0)
            {
                definition.Kind = PropertyKind.Primitive;
            }

            return definition;
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
            var propertyDefinition = FindOrDefineProperty<TProperty>(property, typeof(TProperty), null);
            var validationCollection = (ValidationCollection<T>)propertyDefinition.Validation;
            validationCollection.Add(validation, key, message);
            return this;
        }

        public EntityConfiguration<T> DefineProperty<TProperty>(
            Expression<Func<T, TProperty>> property,
            bool nullable = true,
            IqlType? iqlType = null
        )
        {
            return DefineConvertedProperty(property, null, nullable, iqlType);
        }

        public EntityConfiguration<T> DefineCollectionProperty<TProperty>(
            Expression<Func<T, IEnumerable<TProperty>>> property,
            Expression<Func<T, long?>> countProperty = null
            )
        {
            var iql =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(property);
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
            if (propertyType.IsEnumerableType())
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
            var collection = MapProperty<TElementType, TValueType>(property, true, false, IqlType.Collection, null);
            if (countProperty != null)
            {
                var countPropertyIql =
                    IqlQueryableAdapter.ExpressionToIqlExpressionTree(countProperty);
                var countPropertyDefinition = typeof(T).GetProperty(countPropertyIql.PropertyName);
                if (Nullable.GetUnderlyingType(countPropertyDefinition.PropertyType) != null)
                {
                    var countDefinition = MapProperty<long?, long?>(countProperty, false, true, IqlType.Integer, collection);
                    countDefinition.Kind = countDefinition.Kind | PropertyKind.Count;
                    countDefinition.TypeDefinition = countDefinition.TypeDefinition.ChangeNullable(true);
                }
                else
                {
                    var lambdaExpression = GetLambdaExpression<T>(countPropertyIql.PropertyName);
                    if (countPropertyDefinition.PropertyType == typeof(Int32))
                    {
                        var lambda = (Expression<Func<T, int>>)lambdaExpression;
                        var countDefinition = MapProperty<int, int>(lambda, false, true, IqlType.Integer, collection);
                        countDefinition.Kind = countDefinition.Kind | PropertyKind.Count;
#if TypeScript
                        countDefinition.TypeDefinition = countDefinition.TypeDefinition.ChangeNullable(true);
#endif
                    }
                    else
                    {
                        var lambda = (Expression<Func<T, long>>)lambdaExpression;
                        var countDefinition = MapProperty<long, long>(lambda, false, true, IqlType.Integer, collection);
                        countDefinition.Kind = countDefinition.Kind | PropertyKind.Count;
#if TypeScript
                        countDefinition.TypeDefinition = countDefinition.TypeDefinition.ChangeNullable(true);
#endif
                    }
                }
            }
            EntityConfigurationRelationshipHelper.TryAssignRelationshipToPropertyDefinition(this, collection);
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
            IqlType kind,
            IProperty countRelationship)
        {
            var iql =
                IqlQueryableAdapter.ExpressionToIqlExpressionTree(property);
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<T, TPropertyType, TElementType>;
            if (definition == null)
            {
                definition = new Property<T, TPropertyType, TElementType>(
                    name,
                    false,
                    isCollection,
                    typeof(T),
                    null,
                    readOnly,
                    kind,
                    countRelationship,
                    property);
            }
            else
            {
                definition.ConfigureProperty(
                    name,
                    false,
                    isCollection,
                    typeof(T),
                    typeof(TPropertyType),
                    typeof(TElementType),
                    kind,
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
            EntityConfigurationRelationshipHelper.TryAssignRelationshipToPropertyDefinition(this, definition);
            return definition;
        }

        public OneToRelationshipMap<T, TTarget> HasOne<TTarget>(
            Expression<Func<T, TTarget>> property) where TTarget : class
        {
            return new OneToRelationshipMap<T, TTarget>(
                Builder,
                this,
                Builder.EntityType<TTarget>(),
                RelationshipMapType.One,
                property);
        }

        public ManyToRelationshipMap<T, TTarget> HasMany<TTarget>(
            Expression<Func<T, IEnumerable<TTarget>>> property) where TTarget : class
        {
            return new ManyToRelationshipMap<T, TTarget>(
                Builder,
                this,
                Builder.EntityType<TTarget>(),
                RelationshipMapType.Many,
                property);
        }
    }
}