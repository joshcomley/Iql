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
using System.Threading.Tasks;
using Iql.Entities.Permissions;
using Iql.Entities.PropertyGroups.Dates;
using Iql.Entities.PropertyGroups.Files;
using Iql.Entities.SpecialTypes;

namespace Iql.Entities
{
    public class EntityConfiguration<T> : EntityConfigurationBase, IEntityConfiguration where T : class
    {
        public EntityConfiguration<T> SetGroupPath(string path = null, int order = 0)
        {
            GroupPath = path;
            GroupOrder = order;
            return this;
        }

        public override IUserPermission ParentPermissions => Builder;
        public virtual EntityConfigurationBuilder Builder { get; }

        public SpecialTypeDefinition SpecialTypeDefinition { get; set; }

        public IEntityConfiguration SetManageKind(EntityManageKind manageKind)
        {
            ManageKind = manageKind;
            return this;
        }
        private bool _sanitizersDelayedInitialized;
        private Dictionary<string, EntitySanitizer<T>> _sanitizersDelayed;

        private Dictionary<string, EntitySanitizer<T>> _sanitizers { get { if(!_sanitizersDelayedInitialized) { _sanitizersDelayedInitialized = true; _sanitizersDelayed = new Dictionary<string, EntitySanitizer<T>>(); } return _sanitizersDelayed; } set { _sanitizersDelayedInitialized = true; _sanitizersDelayed = value; } }
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
            get => (DisplayFormatting<T>)base.DisplayFormatting;
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
            DefaultBrowseSortExpression = expression;
            return this;
        }

        public IqlPropertyPath[] ResolveSearchProperties(IqlSearchKind searchKind = IqlSearchKind.Primary, string rootVariableName = null)
        {
            var result = new List<IqlPropertyPath>();
            for (var i = 0; i < Properties.Count; i++)
            {
                var property = Properties[i];
                if (searchKind.HasFlag(IqlSearchKind.Primary) && property.SearchKind == IqlPropertySearchKind.Primary)
                {
                    result.Add(IqlPropertyPath.FromProperty(property, rootVariableName));
                }

                if (searchKind.HasFlag(IqlSearchKind.Secondary) && property.SearchKind == IqlPropertySearchKind.Secondary)
                {
                    result.Add(IqlPropertyPath.FromProperty(property, rootVariableName));
                }

                if (searchKind.HasFlag(IqlSearchKind.Relationships) && property.Relationship != null &&
                    property.Relationship.ThisIsSource
                    && property.Kind.HasFlag(IqlPropertyKind.Relationship))
                {
                    var paths = property.Relationship.OtherEnd.EntityConfiguration
                        .ResolveSearchProperties();
                    for (var j = 0; j < paths.Length; j++)
                    {
                        result.Add(IqlPropertyPath.FromString(
                            Builder,
                            $"{property.Name}/{paths[j].PathToHere}",
                            TypeMetadata,
                            null,
                            rootVariableName));
                    }
                }
            }

            //if (!result.Any() && searchKind == IqlSearchKind.Primary)
            //{
            //    var secondarySearchFields = ResolveSearchProperties(IqlSearchKind.Secondary);
            //    if (secondarySearchFields.Any())
            //    {
            //        var priorityFields = new[] { "FullName", "Name", "Title" };
            //        foreach (var priorityField in priorityFields)
            //        {
            //            var priorityFieldMatch = secondarySearchFields.FirstOrDefault(f => f.Name == priorityField);
            //            if (priorityFieldMatch != null)
            //            {
            //                result.Add(priorityFieldMatch);
            //                return result.ToArray();
            //            }
            //        }

            //        result.Add(secondarySearchFields.First());
            //    }
            //}

            return result.ToArray();
        }

        private IProperty FindOrDefinePropertyInternal(
            string propertyName,
            Type propertyType,
            Type elementType,
            IqlType? iqlType)
        {
            return (IProperty)GetType()
                .GetRuntimeMethods()
                .First(m => m.Name == nameof(FindOrDefineProperty))
                .InvokeGeneric(this, new object[]
                {
                    propertyName,
                    elementType,
                    iqlType
                }, propertyType);
        }

        public IProperty FindOrDefineProperty<TProperty>(string propertyName, Type elementType, IqlType? iqlType)
        {
            //Expression.Lambda()
            var expression = CastLambda<TProperty>(GetLambdaExpression<T>(propertyName));
            var property = FindProperty(propertyName);
            if (property == null)
            {
                if (typeof(TProperty).IsEnumerableType())
                {
                    InvokeDefineCollectionPropertyInternal<TProperty>(
                        typeof(T).GetProperty(propertyName).PropertyType, elementType, propertyName, null);
                }
                else
                {
                    DefineAndGetPropertyInternal(propertyName, expression, null, true, iqlType);
                    //DefineProperty(expression, true, iqlType);
                }
                property = FindProperty(propertyName);
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
            return FindOrDefinePropertyInternal(property.Name, property.PropertyType ?? elementType, property.PropertyType ?? elementType, null);
        }

        public PropertyPath PropertyPath(Expression<Func<T, object>> expression, string key = null)
        {
            var p = IqlPropertyPath.FromLambda(expression, Builder);
            return new PropertyPath(this, p.PathToHere, key);
        }

        public RelationshipDetail<T, TProperty> FindRelationship<TProperty>(Expression<Func<T, TProperty>> propertyName)
        {
            var property = FindPropertyByExpression(propertyName);
            var relationship = FindRelationshipByName(((IMetadata) property).Name);
            return (RelationshipDetail<T, TProperty>)relationship.ThisEnd;
        }

        public CollectionRelationshipDetail<T, TOtherEnd> FindCollectionRelationship<TOtherEnd>(Expression<Func<T, IEnumerable<TOtherEnd>>> propertyName)
        {
            var property = FindPropertyByExpression(propertyName);
            var relationship = FindRelationshipByName(((IMetadata) property).Name);
            return (CollectionRelationshipDetail<T, TOtherEnd>)relationship.ThisEnd;
        }
        private bool _propertyByNameLookupDelayedInitialized;
        private Dictionary<string, IEntityProperty<T>> _propertyByNameLookupDelayed;

        private Dictionary<string, IEntityProperty<T>> _propertyByNameLookup { get { if(!_propertyByNameLookupDelayedInitialized) { _propertyByNameLookupDelayedInitialized = true; _propertyByNameLookupDelayed = new Dictionary<string, IEntityProperty<T>>(); } return _propertyByNameLookupDelayed; } set { _propertyByNameLookupDelayedInitialized = true; _propertyByNameLookupDelayed = value; } }
        public IEntityProperty<T>? FindProperty(string name)
        {
            if (!_propertyByNameLookup.ContainsKey(name))
            {
                var property = (IEntityProperty<T>)Properties.SingleOrDefault(p => p.Name.ToLower() == name.ToLower());
                if (property != null)
                {
                    _propertyByNameLookup.Add(name, property);
                    return property;
                }
                else
                {
                    return null;
                }
            }

            return _propertyByNameLookup[name];
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

        IProperty IEntityConfiguration.FindPropertyByLambdaExpression(LambdaExpression property)
        {
            return FindPropertyByLambdaExpression(property);
        }

        public IEntityProperty<T> FindPropertyByExpression<TProperty>(Expression<Func<T, TProperty>> property)
        {
            return FindPropertyByLambdaExpression(property);
        }

        public IProperty FindNestedPropertyByExpression(Expression<Func<T, object>> property)
        {
            return FindNestedPropertyByLambdaExpression(property);
        }

        public EntityConfiguration<T> HasKey<TKey>(
            Expression<Func<T, TKey>> property,
            IqlType? iqlType = null,
            bool editable = false
        )
        {
            var definedProperty = DefineAndGetProperty(property, null, false, iqlType);
            Key = new EntityKey<T, TKey>();
            Key.CanWrite = editable;
            var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property, Builder).Expression;
            iql.ReturnType = iqlType ?? typeof(TKey).ToIqlType();
            Key.AddProperty(definedProperty);
            TrySetKey(iql.PropertyName);
            return this;
        }

        private void TrySetKey(string propertyName)
        {
            if (Key != null && Key.Properties.Any(p => ((IMetadata) p).Name == propertyName))
            {
                var property = FindProperty(propertyName);
                if (property != null)
                {
                    property.Kind = property.Kind | IqlPropertyKind.Key;
                    //property.Relationship = null;
                }
            }
        }

        public EntityConfiguration<T> HasCompositeKey(
            bool editable,
            params Expression<Func<T, object>>[] properties
        )
        {
            Key = new EntityKey<T, CompositeKey>();
            Key.CanWrite = editable;
            foreach (var property in properties)
            {
                var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property, Builder).Expression;
                var propertyType = typeof(T).GetProperty(iql.PropertyName).PropertyType;
                //iql.ReturnType = typeof(T).getpro.ToIqlType();
                Key.AddProperty(FindOrDefinePropertyInternal(iql.PropertyName, propertyType, propertyType, null));
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

        public IEntityProperty<T> DefineAndGetProperty<TProperty>(Expression<Func<T, TProperty>> property,
            string convertedFromType, bool nullable = true,
            IqlType? iqlType = null)
        {
            var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property, Builder).Expression;
            var name = iql.PropertyName;
            return DefineAndGetPropertyInternal(name, property, convertedFromType, nullable, iqlType);
        }

        public IEntityProperty<T> DefineAndGetPropertyInternal<TProperty>(string name, Expression<Func<T, TProperty>> property, string convertedFromType, bool nullable = true,
            IqlType? iqlType = null)
        {
            if (this.Name == "Client")
            {
                int a = 0;
            }
#if !TypeScript
            if (typeof(TProperty).IsEnumerableType())
            {
                throw new Exception($"Please use {nameof(DefineCollectionProperty)} to define collection properties.");
            }
#endif
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
                definition.Kind = IqlPropertyKind.Relationship;
                definition.Relationship = relationship;
            }

            TrySetKey(name);

            if (definition.Kind == 0)
            {
                definition.Kind = IqlPropertyKind.Primitive;
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
            Expression<Func<T, bool>> failIf,
            string message = null,
            string key = null
            )
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(ResolvePropertyIql(property).PropertyName, typeof(TProperty), null);
            var validationCollection = (ValidationCollection<T>)propertyDefinition.PrimaryProperty.ValidationRules;
            validationCollection.Add(new ValidationRule<T>(failIf, key, message));
            return this;
        }

        public EntityConfiguration<T> RemovePropertyDisplayRuleByExpression<TProperty>(
            Expression<Func<T, TProperty>> property,
            string key)
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(ResolvePropertyIql(property).PropertyName, typeof(TProperty), null);
            return RemovePropertyDisplayRule(propertyDefinition, key);
        }

        public EntityConfiguration<T> RemovePropertyDisplayRule(IPropertyGroup propertyDefinition, string key)
        {
            var ruleCollection = (DisplayRuleCollection<T>) propertyDefinition.PrimaryProperty.DisplayRules;
            ruleCollection?.Remove(key);
            return this;
        }

        public EntityConfiguration<T> DefinePropertyDisplayRuleByExpression<TProperty>(
            Expression<Func<T, TProperty>> property,
            Expression<Func<T, bool>> displayRule,
            string key = null,
            string message = null,
            DisplayRuleKind kind = DisplayRuleKind.DisplayIf,
            DisplayRuleAppliesToKind appliesToKind = DisplayRuleAppliesToKind.NewAndEdit)
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(ResolvePropertyIql(property).PropertyName, typeof(TProperty), null);
            return DefinePropertyDisplayRule(propertyDefinition, displayRule, key, message, kind, appliesToKind);
        }

        public EntityConfiguration<T> DefinePropertyDisplayRule(
            IPropertyGroup propertyDefinition, 
            Expression<Func<T, bool>> displayRule,
            string key = null, 
            string message = null, 
            DisplayRuleKind kind = DisplayRuleKind.DisplayIf, 
            DisplayRuleAppliesToKind appliesToKind = DisplayRuleAppliesToKind.NewAndEdit)
        {
            var primaryProperty = propertyDefinition.PrimaryProperty;
            if(primaryProperty.DisplayRules == null)
            {
                primaryProperty.DisplayRules = new DisplayRuleCollection<T>();
            }
            var ruleCollection = (DisplayRuleCollection<T>) primaryProperty.DisplayRules;
            var rule = ruleCollection.Add(new DisplayRule<T>(displayRule, key, message));
            rule.AppliesToKind = appliesToKind;
            rule.Kind = kind;
            return this;
        }

        private static void AddRelationshipFilterRule<TProperty>(
            Expression<Func<RelationshipFilterContext<T>, Expression<Func<TProperty, bool>>>> filterRule,
            IProperty propertyDefinition,
            string key = null,
            string message = null
            )
        {
            var primaryProperty = propertyDefinition.PrimaryProperty;
            var ruleCollection = (RelationshipRuleCollection<T, TProperty>)primaryProperty.RelationshipFilterRules;
            ruleCollection.Add(new RelationshipFilterRule<T, TProperty>(filterRule, key, message));
        }

        public EntityConfiguration<T> DefineRelationshipFilterRule<TProperty>(
            Expression<Func<T, TProperty>> property,
            Expression<Func<RelationshipFilterContext<T>, Expression<Func<TProperty, bool>>>> filterRule,
            string key = null,
            string message = null)
        {
            var propertyDefinition = FindOrDefineProperty<TProperty>(
                ResolvePropertyIql(property).PropertyName,
                typeof(TProperty),
                null
            );
            AddRelationshipFilterRule(filterRule, propertyDefinition, key, message);
            return this;
        }

        public EntityConfiguration<T> DefineRelationshipCollectionFilterRule<TProperty>(
            Expression<Func<T, IEnumerable<TProperty>>> property,
            Expression<Func<RelationshipFilterContext<T>, Expression<Func<TProperty, bool>>>> filterRule,
            string key = null,
            string message = null)
        {
            var propertyDefinition = FindOrDefineProperty<IEnumerable<TProperty>>(ResolvePropertyIql(property).PropertyName, typeof(TProperty), null);
            AddRelationshipFilterRule(filterRule, propertyDefinition, key, message);
            return this;
        }

        private IqlPropertyExpression ResolvePropertyIql(LambdaExpression property)
        {
            return IqlConverter.Instance.ConvertPropertyLambdaExpressionToIql<T>(property, Builder).Expression;
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
            var iql = IqlConverter.Instance.ConvertPropertyLambdaToIql(property, Builder).Expression;
            var propertyInfo = typeof(T).GetProperty(iql.PropertyName);
            var propertyRuntimeType = propertyInfo.PropertyType;
            var propertyName = iql.PropertyName;
#if TypeScript
            propertyRuntimeType = typeof(TProperty);
#endif
            InvokeDefineCollectionPropertyInternal<TProperty>(propertyRuntimeType, typeof(TProperty), propertyName, countProperty);
            return this;
        }

        private void InvokeDefineCollectionPropertyInternal<TProperty>(
            Type propertyRuntimeType,
            Type elementType,
            string propertyName,
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
                        propertyName, countProperty
                    },
                    propertyRuntimeType, propertyType);
        }

        private EntityConfiguration<T> DefineCollectionPropertyInternal<TValueType, TElementType>(
            string propertyName,
            Expression<Func<T, long?>> countProperty = null
        )
        {
#if TypeScript
            Expression<Func<T, TValueType>> property = _ => _.GetPropertyValueByNameAs<TValueType>(propertyName);
#else
            var property = CastLambda<TValueType>(GetLambdaExpression<T>(propertyName));
#endif
            var collection = MapProperty<TElementType, TValueType>(propertyName, property, true, null, IqlType.Collection, null);
            IProperty countDefinition = null;
            if (countProperty != null)
            {
                var countPropertyIql = IqlConverter.Instance.ConvertPropertyLambdaToIql(countProperty, Builder).Expression;
                var countPropertyDefinition = typeof(T).GetProperty(countPropertyIql.PropertyName);
                if (Nullable.GetUnderlyingType(countPropertyDefinition.PropertyType) != null)
                {
                    countDefinition = MapProperty<long?, long?>(propertyName + "Count", countProperty, false, true, IqlType.Integer, collection);
                    countDefinition.Kind = countDefinition.Kind | IqlPropertyKind.Count;
                    countDefinition.TypeDefinition = countDefinition.TypeDefinition.ChangeNullable(true);
                }
                else
                {
                    var lambdaExpression = GetLambdaExpression<T>(countPropertyIql.PropertyName);
                    if (countPropertyDefinition.PropertyType == typeof(Int32))
                    {
                        var lambda = (Expression<Func<T, int>>)CastLambda<int>(lambdaExpression);
                        countDefinition = MapProperty<int, int>(propertyName + "Count", lambda, false, true, IqlType.Integer, collection);
                        countDefinition.Kind = countDefinition.Kind | IqlPropertyKind.Count;
#if TypeScript
                        countDefinition.TypeDefinition = countDefinition.TypeDefinition.ChangeNullable(true);
#endif
                    }
                    else
                    {
                        var lambda = (Expression<Func<T, long>>)CastLambda<long>(lambdaExpression);
                        countDefinition = MapProperty<long, long>(propertyName + "Count", lambda, false, true, IqlType.Integer, collection);
                        countDefinition.Kind = countDefinition.Kind | IqlPropertyKind.Count;
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
            //return (Expression<Func<TOwner, object>>)(_ => _.GetPropertyValueByName(propertyName));
#if TypeScript
            return (Expression<Func<TOwner, object>>)(_ => _.GetPropertyValueByName(propertyName));
#else
            var param = Expression.Parameter(typeof(TOwner), "o");
            var lambda = Expression.Lambda(Expression.Property(param, propertyName), param);
            return lambda;
#endif
        }

        private Property<T, TPropertyType, TElementType> MapProperty<TElementType, TPropertyType>(
            string propertyName,
            Expression<Func<T, TPropertyType>> property,
            bool isCollection,
            bool? readOnly,
            IqlType kind,
            IProperty countRelationship)
        {
            var definition = FindProperty(propertyName) as Property<T, TPropertyType, TElementType>;
            if (definition == null)
            {
                definition = new Property<T, TPropertyType, TElementType>(
                    this,
                    propertyName,
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
                    propertyName,
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
                _propertiesMap[propertyName] = definition;
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
        public EntityConfiguration<T> SetDisplay(
            string key,
            DisplayConfigurationKind kind,
            Action<EntityConfiguration<T>, DisplayConfiguration> configure
        )
        {
            var configuration = GetOrDefineDisplayConfiguration(kind, key);
            configuration.Kind = kind;
            configure(this, configuration);
            return this;
        }

        public EntityConfiguration<T> SetEditDisplay(
            Action<EntityConfiguration<T>, DisplayConfiguration> configure
        )
        {
            var configuration =
                GetOrDefineDisplayConfiguration(DisplayConfigurationKind.Edit, DisplayConfigurationKeys.Default);
            configuration.Kind = DisplayConfigurationKind.Edit;
            configure(this, configuration);
            return this;
        }

        public string TypeName => Type?.Name;

        public EntityConfiguration<T> AddAlias(string name)
        {
            return (EntityConfiguration<T>)AddAliasInternal(name);
        }

        IEntityConfiguration IEntityConfiguration.AddAlias(string name)
        {
            return AddAlias(name);
        }

        DisplayConfiguration IEntityConfiguration.GetOrDefineDisplayConfigurationBase<TEntity>(DisplayConfigurationKind kind, string key, Action<EntityConfiguration<TEntity>, DisplayConfiguration> configure = null)
        {
            return GetOrDefineDisplayConfiguration(kind, key, (configuration, displayConfiguration) =>
            {
                if (configure != null)
                {
                    configure((EntityConfiguration<TEntity>)(object)configuration, displayConfiguration);
                }
            });
        }

        public DisplayConfiguration GetOrDefineDisplayConfiguration(
            DisplayConfigurationKind kind,
            string key,
            Action<EntityConfiguration<T>, DisplayConfiguration> configure = null
        )
        {
            var definition = GetDisplayConfiguration(kind, key) ??
                             new DisplayConfiguration(kind, null, key);
            if (!DisplayConfigurations.Contains(definition))
            {
                DisplayConfigurations.Add(definition);
            }
            definition.AutoGenerated = false;
            if (configure != null)
            {
                configure(this, definition);
            }
            return definition;
        }

        public EntityConfiguration<T> SetReadDisplay(
            Action<EntityConfiguration<T>, DisplayConfiguration> configure
        )
        {
            var configuration =
                GetOrDefineDisplayConfiguration(DisplayConfigurationKind.Read, DisplayConfigurationKeys.Default);
            configuration.Kind = DisplayConfigurationKind.Read;
            configure(this, configuration);
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
            Action<IGeographicPoint> configure = null)
        {
            var geo = new GeographicPoint(
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
            Guid guid,
            Action<File<T>> configure = null
        )
        {
            var fileProperty = FindPropertyByExpression(fileUrlProperty);
            // MediaKey should support preview variable
            var file = new File<T>(guid, fileProperty);
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

        public IProperty PrimaryProperty
        {
            get { return null; }
        }

        public bool IsTypeGroup => true;
        public IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.EntityConfiguration;
        public PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            return new PropertyGroupMetadata[] { };
        }
    }
}