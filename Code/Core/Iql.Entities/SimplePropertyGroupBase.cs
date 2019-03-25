namespace Iql.Entities
{
    public abstract class SimplePropertyGroupBase<T> : PropertyGroupBase<T>, ISimpleProperty
        where T : IConfigurable<T>
    {
        protected SimplePropertyGroupBase(IEntityConfiguration entityConfiguration, string key) : base(entityConfiguration, key)
        {
        }
    }
}