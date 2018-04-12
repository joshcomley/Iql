using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Iql.Queryable.Data.EntityConfiguration.DisplayFormatting;
using Iql.Queryable.Data.EntityConfiguration.Relationships;
using Iql.Queryable.Data.EntityConfiguration.Validation;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntityConfiguration : IEntityMetadata
    {
        bool HasNonKeyFields();
        bool HasRelationshipKeys { get; }
        EntityConfigurationBuilder Builder { get; }
        string GetDisplayText(object entity, string key = null);
        IProperty[] ResolveSearchProperties(PropertySearchKind searchKind = PropertySearchKind.Primary);
        IEntityValidationResult ValidateEntity(object entity);
        IPropertyValidationResult ValidateEntityPropertyByExpression<TProperty>(object entity,
            Expression<Func<object, TProperty>> property);
        IPropertyValidationResult ValidateEntityPropertyByName(object entity, string property);
        IPropertyValidationResult ValidateEntityProperty(object entity, IProperty property);
        IProperty FindPropertyByExpression(Expression<Func<object, object>> expression);
        IProperty FindPropertyByIqlExpression(IqlPropertyExpression propertyExpression);
        IProperty FindPropertyByLambdaExpression(LambdaExpression expression);
        IDisplayFormatting DisplayFormatting { get; }
        IValidationCollection EntityValidation { get; }
        List<IProperty> Properties { get; }
        List<IRelationship> Relationships { get; set; }
        IEntityKey Key { get; }
        Type Type { get; }
        IProperty FindOrDefineProperty<TProperty>(LambdaExpression expression, Type elementType, IqlType? iqlType = null);
        IProperty FindProperty(string name);
        IProperty FindOrDefinePropertyByName(string name, Type elementType);
        RelationshipMatch FindRelationship(string propertyName);
        List<RelationshipMatch> AllRelationships();
        bool EntityHasKey(object entity, CompositeKey key);
        bool KeysMatch(object left, object right);
        CompositeKey GetCompositeKey(object entity);
    }
}