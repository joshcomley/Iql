using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities.Validation.Validation;

namespace Iql.Entities
{
    public interface IEntityConfiguration : IEntityMetadata, IConfiguration, IPropertyContainer
    {
        IProperty TitleProperty { get; }
        IProperty PreviewProperty { get; }
        IEntityConfiguration SetManageKind(EntityManageKind manageKind);
        /// <summary>
        /// Determines whether this entity type has any fields that aren't key fields
        /// </summary>
        /// <returns></returns>
        object GetVersion(object entity);
        bool HasNonKeyFields();
        bool HasRelationshipKeys { get; }
        EntityConfigurationBuilder Builder { get; }
        string GetDisplayText(object entity, string key = null);
        IEntityConfiguration SetDefaultSortExpression(string expression);
        IPropertyGroup[] GetDisplayConfiguration(DisplayConfigurationKind kind, bool appendMissingProperties = true);
        IProperty[] ResolveSearchProperties(PropertySearchKind searchKind = PropertySearchKind.Primary);
        IEntityValidationResult ValidateEntity(object entity);
        IPropertyValidationResult ValidateEntityPropertyByExpression<TProperty>(object entity,
            Expression<Func<object, TProperty>> property);
        IPropertyValidationResult ValidateEntityPropertyByName(object entity, string property);
        IPropertyValidationResult ValidateEntityProperty(object entity, IProperty property);
        IProperty FindPropertyByExpression(Expression<Func<object, object>> expression);
        IProperty[] FindPropertiesByHint(string hint);
        IProperty FindNestedPropertyByIqlExpression(IqlPropertyExpression propertyExpression);
        IProperty FindNestedPropertyByLambdaExpression(LambdaExpression expression);
        IEntityConfiguration AddSanitizer(Action<object> expression, string key = null);
        IEntityConfiguration SetGeographyResolver(Func<object, Task<Geography.Geography>> expression);
        Task<Geography.Geography> ResolveGeographyAsync(object entity);
        Type Type { get; }
        IProperty FindOrDefineProperty<TProperty>(LambdaExpression expression, Type elementType, IqlType? iqlType = null);
        IProperty FindNestedProperty(string name);
        IProperty FindProperty(string name);
        IProperty FindOrDefinePropertyByName(string name, Type elementType);
        EntityRelationship FindRelationshipByName(string propertyName);
        List<EntityRelationship> AllRelationships();
        bool EntityHasKey(object entity, CompositeKey key);
        bool KeysMatch(object left, object right);
        CompositeKey GetCompositeKey(object entity);
    }
}