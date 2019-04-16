using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities;
using Iql.Entities.Geography;
using Iql.Entities.SpecialTypes;
using Iql.Entities.Validation.Validation;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    [DebuggerDisplay("{Name} - {SetName}")]
    public class EntityConfiguration : EntityConfigurationBase, IEntityConfiguration
    {
        protected override IEntityConfigurationContainer ConfigurationContainer => _configurationContainer;

        private  IEntityConfigurationContainer _configurationContainer;
        IEntityConfiguration IEntityConfigurationItem.EntityConfiguration => this;
        public override IUserPermission ParentPermissions => Builder;

        internal void SetConfigurationProvider(IEntityConfigurationContainer container)
        {
            _configurationContainer = container;
        }

        public DisplayConfiguration GetOrDefineDisplayConfigurationBase<T>(DisplayConfigurationKind kind, string key,
            Action<EntityConfiguration<T>, DisplayConfiguration> configure = null) where T : class
        {
            throw new NotImplementedException();
        }

        public override string TitlePropertyName { get; set; }
        public SpecialTypeDefinition SpecialTypeDefinition { get; set; }

        public IEntityConfiguration SetManageKind(EntityManageKind manageKind)
        {
            throw new NotImplementedException();
        }

        public EntityConfigurationBuilder Builder { get; }

        public string GetDisplayText(object entity, string key = null)
        {
            throw new NotImplementedException();
        }

        public IEntityConfiguration SetDefaultSortExpression(string expression)
        {
            throw new NotImplementedException();
        }

        public IProperty[] ResolveSearchProperties(IqlSearchKind searchKind = IqlSearchKind.Primary)
        {
            throw new NotImplementedException();
        }

        public Task<IEntityValidationResult> ValidateEntityAsync(object entity)
        {
            throw new NotImplementedException();
        }

        public Task<IPropertyValidationResult> ValidateEntityPropertyByExpressionAsync<TProperty>(object entity, Expression<Func<object, TProperty>> property)
        {
            throw new NotImplementedException();
        }

        public Task<IPropertyValidationResult> ValidateEntityPropertyByNameAsync(object entity, string property)
        {
            throw new NotImplementedException();
        }

        public Task<IPropertyValidationResult> ValidateEntityPropertyAsync(object entity, IProperty property)
        {
            throw new NotImplementedException();
        }

        public IProperty FindPropertyByExpression(Expression<Func<object, object>> expression)
        {
            throw new NotImplementedException();
        }

        public IProperty[] FindPropertiesByHint(string hint)
        {
            throw new NotImplementedException();
        }

        public IProperty FindPropertyByLambdaExpression(LambdaExpression property)
        {
            throw new NotImplementedException();
        }

        public IEntityConfiguration AddSanitizer(Action<object> expression, string key = null)
        {
            throw new NotImplementedException();
        }

        public IEntityConfiguration SetGeographyResolver(Func<object, Task<Geography>> expression)
        {
            throw new NotImplementedException();
        }

        public async Task<Geography> ResolveGeographyAsync(object entity)
        {
            throw new NotImplementedException();
        }

        public IProperty FindOrDefineProperty<TProperty>(string propertyName, Type elementType, IqlType? iqlType = null)
        {
            throw new NotImplementedException();
        }

        public IProperty FindProperty(string name)
        {
            return Properties.FirstOrDefault(p => p.Name == name);
        }

        public IProperty FindOrDefinePropertyByName(string name, Type elementType)
        {
            return FindProperty(name);
        }

        public EntityConfiguration(IEntityConfigurationContainer configurationContainer)
            : base(configurationContainer)
        {
        }

        public IqlPropertyGroupKind GroupKind { get; } = IqlPropertyGroupKind.EntityConfiguration;
        public PropertyGroupMetadata[] GetPropertyGroupMetadata()
        {
            throw new NotImplementedException();
        }
    }
}