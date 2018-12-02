namespace Iql.Entities.Relationships
{
    public interface IMappingBase
    {
        IRelationshipDetail Container { get; }
        IPropertyGroup Property { get; }
        IqlLambdaExpression Expression { get; set; }
        bool UseForFiltering { get; set; }
        void SetValue(object entity, object value);
    }

    public abstract class MappingBase : IMappingBase
    {
        public IRelationshipDetail Container { get; protected set; }
        IPropertyGroup IMappingBase.Property => ResolvePropertyGroup();
        public IqlLambdaExpression Expression { get; set; }
        public bool UseForFiltering { get; set; }
        public abstract void SetValue(object entity, object value);
        protected abstract IPropertyGroup ResolvePropertyGroup();
    }

    public abstract class Mapping<T> : MappingBase, IMapping<T>
        where T : IPropertyGroup
    {
        public T Property { get; set; }

        protected override IPropertyGroup ResolvePropertyGroup()
        {
            return Property;
        }

        protected Mapping(IRelationshipDetail container)
        {
            Container = container;
        }
    }
}