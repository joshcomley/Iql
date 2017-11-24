namespace Iql.Queryable.Data.EntityConfiguration
{
    public abstract class EntityConfigurationBase
    {
        internal abstract void TryAssignRelationshipToProperty(string property, bool tryAssignOtherEnd = true);
        internal abstract void TryAssignRelationshipToPropertyDefinition(IProperty definition, bool tryAssignOtherEnd = true);
    }
}