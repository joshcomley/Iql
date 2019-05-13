using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Entities.Functions;
using Iql.Entities.SpecialTypes;

namespace Iql.Entities
{
    public interface IEntityConfiguration : IEntityMetadata, IPropertyContainer, IIqlTypeMetadataProvider
    {
        string TypeName { get; }
        IqlMethod FindMethod(string name, bool? ensure = null, Action<IqlMethod> configure = null);
        IEntityConfiguration AddMethod(IqlMethod method);
        IProperty[] TryMatchProperty(params string[] names);
        IEnumerable<DisplayConfiguration> DisplayConfigurationsFor(DisplayConfigurationKind kind);
        DisplayConfiguration GetFullDisplayConfiguration(DisplayConfigurationKind? kind = null);
        DisplayConfiguration FindDisplayConfiguration(DisplayConfigurationKind? kind = null);
        DisplayConfiguration GetDisplayConfiguration(DisplayConfigurationKind kind, params string[] keys);
        DisplayConfiguration GetOrDefineDisplayConfigurationBase<T>(DisplayConfigurationKind kind, string key, Action<EntityConfiguration<T>, DisplayConfiguration> configure = null)
            where T : class;
        SpecialTypeDefinition SpecialTypeDefinition { get; set; }
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
        bool IsPivot { get; }
        EntityConfigurationBuilder Builder { get; }
        string GetDisplayText(object entity, string key = null);
        IEntityConfiguration SetDefaultSortExpression(string expression);
        IPropertyGroup[] BuildDisplayConfiguration(DisplayConfiguration configuration, bool? doNotAppendMissingProperties = null, bool? includeReadHiddenProperties = null);
        IqlPropertyPath[] ResolveSearchProperties(IqlSearchKind searchKind = IqlSearchKind.Primary, string rootVariableName = null);
        IProperty[] FindPropertiesByHint(string hint);
        IProperty FindPropertyByLambdaExpression(LambdaExpression property);
        IProperty FindNestedPropertyByIqlExpression(IqlPropertyExpression propertyExpression);
        IProperty FindNestedPropertyByLambdaExpression(LambdaExpression expression);
        IEntityConfiguration AddSanitizer(Action<object> expression, string key = null);
        IEntityConfiguration SetGeographyResolver(Func<object, Task<Geography.Geography>> expression);
        Task<Geography.Geography> ResolveGeographyAsync(object entity);
        Type Type { get; }
        IProperty FindOrDefineProperty<TProperty>(string propertyName, Type elementType, IqlType? iqlType = null);
        IProperty FindNestedProperty(string name);
        IProperty FindProperty(string name);
        IProperty FindOrDefinePropertyByName(string name, Type elementType);
        EntityRelationship FindRelationshipByName(string propertyName);
        List<EntityRelationship> AllRelationships();
        bool EntityHasKey(object entity, CompositeKey key);
        bool KeysMatch(object left, object right);
        CompositeKey GetCompositeKey(object entity);
        string GetCompositeKeyString(object entity);
    }
}