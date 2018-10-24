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
        protected override Entities.IEntityConfigurationContainer ConfigurationContainer => _configurationContainer;

        private EntityConfigurationBuilder _builder;
        private  Entities.IEntityConfigurationContainer _configurationContainer;
        IEntityConfiguration IEntityConfigurationItem.EntityConfiguration => this;

        internal void SetConfigurationProvider(Entities.IEntityConfigurationContainer container)
        {
            _configurationContainer = container;
        }

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

        public IProperty[] ResolveSearchProperties(PropertySearchKind searchKind = PropertySearchKind.Primary)
        {
            throw new NotImplementedException();
        }

        public IEntityValidationResult ValidateEntity(object entity)
        {
            throw new NotImplementedException();
        }

        public IPropertyValidationResult ValidateEntityPropertyByExpression<TProperty>(object entity, Expression<Func<object, TProperty>> property)
        {
            throw new NotImplementedException();
        }

        public IPropertyValidationResult ValidateEntityPropertyByName(object entity, string property)
        {
            throw new NotImplementedException();
        }

        public IPropertyValidationResult ValidateEntityProperty(object entity, IProperty property)
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

        public EntityConfiguration(Entities.IEntityConfigurationContainer configurationContainer)
            : base(configurationContainer)
        {
        }
    }
}