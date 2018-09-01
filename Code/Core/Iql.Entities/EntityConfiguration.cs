using Iql.Conversion;
using Iql.Entities.DisplayFormatting;
using Iql.Entities.Extensions;
using Iql.Entities.Geography;
using Iql.Entities.NestedSets;
using Iql.Entities.Relationships;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;
using Iql.Entities.Sanitization;
using Iql.Entities.Validation;
using Iql.Entities.Validation.Validation;
using Iql.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.SpecialTypes;

namespace Iql.Entities
{
    public class EntityConfiguration<T> : EntityConfigurationBase, IEntityConfiguration where T : class
    {
        public virtual EntityConfigurationBuilder Builder { get; }
        IEntityConfiguration IEntityConfigurationItem.EntityConfiguration => this;
        private class DefaultValuePlaceholder { }

        private static readonly DefaultValuePlaceholder DefaultValuePlaceholderInstance = new DefaultValuePlaceholder();

        public SpecialTypeDefinition SpecialTypeDefinition { get; set; }

        public IEntityConfiguration SetManageKind(EntityManageKind manageKind)
        {
            ManageKind = manageKind;
            return this;
        }

        private readonly Dictionary<string, EntitySanitizer<T>> _sanitizers = new Dictionary<string, EntitySanitizer<T>>();
        public IEntityConfiguration AddSanitizer(Action<T> expression, string key = null)
        {
            key = key ?? Guid.NewGuid().ToString();
            if (!_sanitizers.ContainsKey(key))
            {
                _sanitizers.Add(key, new EntitySanitizer<T>());
            }

            var sanitizer = _sanitizers[key];
            sanitizer.Key = key;
            sanitizer.Run = expression;
            return this;
        }

        IEntityConfiguration IEntityConfiguration.AddSanitizer(Action<object> expression, string key = null)
        {
            return AddSanitizer(expression, key);
        }

        public IEnumerable<EntitySanitizer<T>> Sanitizers()
        {
            return _sanitizers.Values;
        }

        private GeographyResolver<T> _geographyResolver = null;
        public async Task<Geography.Geography> ResolveGeographyAsync(T entity)
        {
            if (_geographyResolver == null)
            {
                return null;
            }

            return await _geographyResolver.ResolveAsync(entity);
        }

        Task<Geography.Geography> IEntityConfiguration.ResolveGeographyAsync(object entity)
        {
            return ResolveGeographyAsync((T)entity);
        }

        public IEntityConfiguration SetGeographyResolver(Func<T, Task<Geography.Geography>> expression)
        {
            _geographyResolver = new GeographyResolver<T>(expression);
            return this;
        }

        IEntityConfiguration IEntityConfiguration.SetGeographyResolver(
            Func<object, Task<Geography.Geography>> expression)
        {
            SetGeographyResolver(expression);
            return this;
        }

        public new DisplayFormatting<T> DisplayFormatting
        {
            get => (DisplayFormatting<T>) base.DisplayFormatting;
            set => base.DisplayFormatting = value;
        }

        public new ValidationCollection<T> EntityValidation
        {
            get => (ValidationCollection<T>)base.EntityValidation;
            set => base.EntityValidation = value;
        }

        public EntityConfiguration(EntityConfigurationBuilder builder = null) : base(builder)
        {
            Type = typeof(T);
            DisplayFormatting = new DisplayFormatting<T>(this);
            Builder = builder;
            EntityValidation = new ValidationCollection<T>();
            Properties = new List<IProperty>();
            Name = Type.Name;
        }

        public string GetDisplayText(T entity, string key = null)
        {
            return DisplayFormatting.TryFormat(entity, key);
        }

        string IEntityConfiguration.GetDisplayText(object entity, string key = null)
        {
            return GetDisplayText((T)entity, key);
        }

        public IEntityConfiguration SetDefaultSortExpression(string expression)
        {
            DefaultSortExpression = expression;
            return this;
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
            var property = FindPropertyByLambdaExpression(expression);
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
                if (!validation.Run(entity))
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
            foreach (var validation in property.ValidationRules.All)
            {
                if (!validation.Run(entity))
                {
                    validationResult.AddFailure(validation.Key, validation.Message);
                }
            }

            if (!property.Kind.HasFlag(PropertyKind.Count) && !property.Kind.HasFlag(PropertyKind.Key))
            {
                var propertyValue = property.GetValue(entity);
                if (!validationResult.HasValidationFailures() &&
                PropertyValueIsIllegallyEmpty(property, entity, propertyValue)
            )
                {
                    if (!hasSetDefaultValue)
                    {
                        // Mimic default values for 
                        object newValue = DefaultValuePlaceholderInstance;
                        if (property.TypeDefinition.ConvertedFromType == KnownPrimitiveTypes.Guid ||
                            property.TypeDefinition.Kind == IqlType.Date ||
                            property.TypeDefinition.Kind == IqlType.Enum)
                        {
                            newValue = property.TypeDefinition.DefaultValue();
                        }
                        if (!Equals(newValue, DefaultValuePlaceholderInstance))
                        {
                            property.SetValue(entity, newValue);
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

        private static bool PropertyValueIsIllegallyEmpty(IProperty property, object entity, object propertyValue)
        {
            if (property.IsReadOnly)
            {
                return false;
            }

            if (property.TypeDefinition.Nullable && property.Nullable != false)
            {
                return false;
            }

            if (property.Kind.HasFlag(PropertyKind.Relationship) &&
                propertyValue == null)
            {
                if (!property.Relationship.ThisIsTarget)
                {
                    var properties = property.Relationship.ThisEnd.Constraints;
                    for (var i = 0; i < properties.Length; i++)
                    {
                        var constraint = properties[i];
                        var constraintValue = constraint.GetValue(entity);
                        if (Equals(null, constraintValue) ||
                            Equals(constraint.TypeDefinition.DefaultValue(), constraintValue))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (Equals(null, propertyValue) && property.TypeDefinition.Nullable == false)
            {
                object defaultValue;
                switch (property.TypeDefinition.Kind)
                {
                    case IqlType.Integer:
                    case IqlType.Decimal:
                    case IqlType.Enum:
                    case IqlType.Boolean:
                    case IqlType.Date:
                        defaultValue = property.TypeDefinition.DefaultValue();
                        break;
                    default:
                        return true;
                }
                property.SetValue(entity, defaultValue);
                propertyValue = defaultValue;
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
            //Expression.Lambda()
            var expression = CastLambda<TProperty>(lambda);
            var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(expression).Expression;
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

        private Expression<Func<T, TProperty>> CastLambda<TProperty>(LambdaExpression lambda)
        {
#if TypeScript
            return (Expression<Func<T, TProperty>>)lambda;
#else
            return (Expression<Func<T, TProperty>>)Expression.Lambda(Expression.Convert(lambda.Body, typeof(TProperty)), lambda.Parameters);
#endif
        }

        public IProperty FindOrDefinePropertyByName(string name, Type elementType)
        {
            var property = typeof(T).GetProperty(name);
            return FindOrDefinePropertyInternal(GetLambdaExpression<T>(property.Name), property.PropertyType ?? elementType, property.PropertyType ?? elementType, null);
        }

        public PropertyPath PropertyPath(Expression<Func<T, object>> expression, string key = null)
        {
            var p = IqlPropertyPath.FromLambda(expression, this);
            return new PropertyPath(this, p.PathToHere, key);
        }

        public RelationshipDetail<T, TProperty> FindRelationship<TProperty>(Expression<Func<T, TProperty>> propertyName)
        {
            var property = FindPropertyByExpression(propertyName);
            var relationship = FindRelationshipByName(property.Name);
            return (RelationshipDetail<T, TProperty>)relationship.ThisEnd;
        }

        public CollectionRelationshipDetail<T, TOtherEnd> FindCollectionRelationship<TOtherEnd>(Expression<Func<T, IEnumerable<TOtherEnd>>> propertyName)
        {
            var property = FindPropertyByExpression(propertyName);
            var relationship = FindRelationshipByName(property.Name);
            return (CollectionRelationshipDetail<T, TOtherEnd>)relationship.ThisEnd;
        }

        public IEntityProperty<T> FindProperty(string name)
        {
            return (IEntityProperty<T>)Properties.SingleOrDefault(p => p.Name.ToLower() == name.ToLower());
        }
        IProperty IEntityConfiguration.FindProperty(string name)
        {
            return FindProperty(name);
        }

        public IEntityProperty<T>[] FindPropertiesByHint(string hint)
        {
            return Properties.Where(p => p.HasHint(hint)).Select(p => (IEntityProperty<T>)p).ToArray();
        }

        IProperty[] IEntityConfiguration.FindPropertiesByHint(string hint)
        {
            return FindPropertiesByHint(hint).Select(p => (IProperty)p).ToArray();
        }

        public IEntityProperty<T> FindPropertyByIqlExpression(IqlPropertyExpression propertyExpression)
        {
            return (IEntityProperty<T>)FindNestedPropertyByIqlExpression(propertyExpression);
        }

        public IEntityProperty<T> FindPropertyByLambdaExpression(LambdaExpression property)
        {
            return (IEntityProperty<T>)FindNestedPropertyByLambdaExpression(property);
        }


        public IEntityProperty<T> FindPropertyByExpression<TProperty>(Expression<Func<T, TProperty>> property)
        {
            return FindPropertyByLambdaExpression(property);
        }

        public IProperty FindNestedPropertyByExpression(Expression<Func<T, object>> property)
        {
            return FindNestedPropertyByLambdaExpression(property);
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
            var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property).Expression;
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
                var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property).Expression;
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
            Action<IEntityProperty<T>> configure)
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

        public IEntityProperty<T> DefineAndGetProperty<TProperty>(Expression<Func<T, TProperty>> property, string convertedFromType, bool nullable = true,
            IqlType? iqlType = null)
        {
#if !TypeScript
            if (typeof(TProperty).IsEnumerableType())
            {
                throw new Exception($"Please use {nameof(DefineCollectionProperty)} to define collection properties.");
            }
#endif
            var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property).Expression;
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<T, TProperty, TProperty>
                             ?? new Property<T, TProperty, TProperty>(
                                 this,
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
            definition.Nullable = nullable;
            definition.TypeDefinition = definition.TypeDefinition.ChangeConvertedFromType(convertedFromType);
            if (iqlType != null && iqlType != IqlType.Unknown)
            {
                definition.TypeDefinition = definition.TypeDefinition.ChangeKind(iqlType.Value);
            }

            //definition.GetValue = property.Compile();
            //definition.PropertyGetterExpression = property;
            if (!Properties.Contains(definition))
            {
                Properties.Add(definition);
                _propertiesMap[name] = definition;
            }

            var relationship = FindRelationshipByName(name);
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
            string message = null,
            string key = null
            )
        {
            EntityValidation.Add(new ValidationRule<T>(validation, key, message));
            return this;
        }

        public EntityConfiguration<T> DefinePropertyValidation<TProperty>(
            Expression<Func<T, TProperty>> property,
            Expression<Func<T, bool>> validation,
            string message = null,
            string key = null
            )
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(property, typeof(TProperty), null);
            var validationCollection = (ValidationCollection<T>)propertyDefinition.ValidationRules;
            validationCollection.Add(new ValidationRule<T>(validation, key, message));
            return this;
        }

        public EntityConfiguration<T> DefinePropertyDisplayRule<TProperty>(
            Expression<Func<T, TProperty>> property,
            Expression<Func<T, bool>> displayRule,
            string key = null,
            string message = null,
            DisplayRuleKind kind = DisplayRuleKind.DisplayIf,
            DisplayRuleAppliesToKind appliesToKind = DisplayRuleAppliesToKind.NewAndEdit)
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(property, typeof(TProperty), null);
            var ruleCollection = (DisplayRuleCollection<T>)propertyDefinition.DisplayRules;
            var rule = ruleCollection.Add(new DisplayRule<T>(displayRule, key, message));
            rule.AppliesToKind = appliesToKind;
            rule.Kind = kind;
            return this;
        }

        private static void AddRelationshipFilterRule<TProperty>(Expression<Func<RelationshipFilterContext<T>, Expression<Func<TProperty, bool>>>> filterRule,
            IProperty propertyDefinition,
            string key = null,
            string message = null
            )
        {
            var ruleCollection = (RelationshipRuleCollection<T, TProperty>)propertyDefinition.RelationshipFilterRules;
            ruleCollection.Add(new RelationshipFilterRule<T, TProperty>(filterRule, key, message));
        }

        public EntityConfiguration<T> DefineRelationshipFilterRule<TProperty>(
            Expression<Func<T, TProperty>> property,
            Expression<Func<RelationshipFilterContext<T>, Expression<Func<TProperty, bool>>>> filterRule,
            string key = null,
            string message = null)
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(property, typeof(TProperty), null);
            AddRelationshipFilterRule(filterRule, propertyDefinition, key, message);
            return this;
        }

        public EntityConfiguration<T> DefineRelationshipCollectionFilterRule<TProperty>(
            Expression<Func<T, IEnumerable<TProperty>>> property,
            Expression<Func<RelationshipFilterContext<T>, Expression<Func<TProperty, bool>>>> filterRule,
            string key = null,
            string message = null)
        {
            var propertyDefinition = FindOrDefineProperty<IEnumerable<TProperty>>(property, typeof(TProperty), null);
            AddRelationshipFilterRule(filterRule, propertyDefinition, key, message);
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
            var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property).Expression;
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
                    propertyRuntimeType, propertyType);
        }

        private EntityConfiguration<T> DefineCollectionPropertyInternal<TValueType, TElementType>(
            Expression<Func<T, TValueType>> property,
            Expression<Func<T, long?>> countProperty = null
        )
        {
            var collection = MapProperty<TElementType, TValueType>(property, true, null, IqlType.Collection, null);
            IProperty countDefinition = null;
            if (countProperty != null)
            {
                var countPropertyIql = IqlConverter.Instance.ConvertPropertyLambdaToIql(countProperty).Expression;
                var countPropertyDefinition = typeof(T).GetProperty(countPropertyIql.PropertyName);
                if (Nullable.GetUnderlyingType(countPropertyDefinition.PropertyType) != null)
                {
                    countDefinition = MapProperty<long?, long?>(countProperty, false, true, IqlType.Integer, collection);
                    countDefinition.Kind = countDefinition.Kind | PropertyKind.Count;
                    countDefinition.TypeDefinition = countDefinition.TypeDefinition.ChangeNullable(true);
                }
                else
                {
                    var lambdaExpression = GetLambdaExpression<T>(countPropertyIql.PropertyName);
                    if (countPropertyDefinition.PropertyType == typeof(Int32))
                    {
                        var lambda = (Expression<Func<T, int>>)lambdaExpression;
                        countDefinition = MapProperty<int, int>(lambda, false, true, IqlType.Integer, collection);
                        countDefinition.Kind = countDefinition.Kind | PropertyKind.Count;
#if TypeScript
                        countDefinition.TypeDefinition = countDefinition.TypeDefinition.ChangeNullable(true);
#endif
                    }
                    else
                    {
                        var lambda = (Expression<Func<T, long>>)lambdaExpression;
                        countDefinition = MapProperty<long, long>(lambda, false, true, IqlType.Integer, collection);
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
            bool? readOnly,
            IqlType kind,
            IProperty countRelationship)
        {
            var iql =
                IqlConverter.Instance.ConvertPropertyLambdaToIql(property).Expression;
            var name = iql.PropertyName;
            var definition = FindProperty(name) as Property<T, TPropertyType, TElementType>;
            if (definition == null)
            {
                definition = new Property<T, TPropertyType, TElementType>(
                    this,
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
                    this,
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

        //public ManyToRelationshipMap<T, TTarget> HasMany<TTarget>(
        //    Expression<Func<T, IEnumerable<TTarget>>> property) where TTarget : class
        //{
        //    return new ManyToRelationshipMap<T, TTarget>(
        //        Builder,
        //        this,
        //        Builder.EntityType<TTarget>(),
        //        RelationshipMapType.Many,
        //        property);
        //}

        public EntityConfiguration<T> SetEditDisplay(params Func<EntityConfiguration<T>, IPropertyGroup>[] properties)
        {
            var coll = new List<IPropertyGroup>();
            foreach (var property in properties)
            {
                coll.Add(property(this));
            }
            EditDisplay = coll.ToList();
            return this;
        }

        public EntityConfiguration<T> SetReadDisplay(params Func<EntityConfiguration<T>, IPropertyGroup>[] properties)
        {
            var coll = new List<IPropertyGroup>();
            foreach (var property in properties)
            {
                coll.Add(property(this));
            }
            ReadDisplay = coll.ToList();
            return this;
        }

        public IPropertyCollection PropertyCollection(params Func<EntityConfiguration<T>, IPropertyGroup>[] properties)
        {
            var coll = new PropertyCollection(this);
            foreach (var property in properties)
            {
                coll.Properties.Add(property(this));
            }
            return coll;
        }

        public EntityConfiguration<T> HasGeographic(
            Expression<Func<T, object>> longitudeProperty,
            Expression<Func<T, object>> latitudeProperty,
            string key = null,
            Action<IGeographic> configure = null)
        {
            var geo = new Geographic(
                FindPropertyByExpression(longitudeProperty),
                FindPropertyByExpression(latitudeProperty),
                key);
            Geographics.Add(geo);
            if (configure != null)
            {
                configure(geo);
            }
            return this;
        }

        public EntityConfiguration<T> HasDateRange(
            Expression<Func<T, object>> startDateProperty,
            Expression<Func<T, object>> endDateProperty,
            string key = null,
            Action<IDateRange> configure = null
        )
        {
            var dateRange = new DateRange(
                FindPropertyByExpression(startDateProperty),
                FindPropertyByExpression(endDateProperty),
                key);
            DateRanges.Add(dateRange);
            if (configure != null)
            {
                configure(dateRange);
            }
            return this;
        }

        public File<T>[] FindFiles(Func<File<T>, bool> filter)
        {
            return Files.Select(f => (File<T>)f).Where(filter).ToArray();
        }

        public EntityConfiguration<T> HasFile(
            Expression<Func<T, object>> fileUrlProperty,
            Action<File<T>> configure = null
        )
        {
            var fileProperty = FindPropertyByExpression(fileUrlProperty);
            // MediaKey should support preview variable
            var file = new File<T>(fileProperty);
            Files.Add(file);
            if (configure != null)
            {
                configure(file);
            }
            return this;
        }

        public EntityConfiguration<T> HasNestedSet(
            Expression<Func<T, object>> leftProperty,
            Expression<Func<T, object>> rightProperty,
            Expression<Func<T, object>> leftOfProperty = null,
            Expression<Func<T, object>> rightOfProperty = null,
            Expression<Func<T, object>> keyProperty = null,
            Expression<Func<T, object>> levelProperty = null,
            Expression<Func<T, object>> parentIdProperty = null,
            Expression<Func<T, object>> parentProperty = null,
            Expression<Func<T, object>> idProperty = null,
            string setKey = null,
            string key = null,
            Action<INestedSet> configure = null)
        {
            var nestedSet = new NestedSet(
                FindPropertyByExpression(leftProperty),
                FindPropertyByExpression(rightProperty),
                FindPropertyByExpression(leftOfProperty),
                FindPropertyByExpression(rightOfProperty),
                FindPropertyByExpression(keyProperty),
                FindPropertyByExpression(levelProperty),
                FindPropertyByExpression(parentIdProperty),
                FindPropertyByExpression(parentProperty),
                FindPropertyByExpression(idProperty),
                setKey,
                key);
            NestedSets.Add(nestedSet);
            if (configure != null)
            {
                configure(nestedSet);
            }
            return this;
        }
    }
}