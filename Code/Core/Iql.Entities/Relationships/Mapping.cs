namespace Iql.Entities.Relationships
{
    public class Mapping<T> : IMapping<T>
    {
        public IRelationshipDetail Container { get; }
        public T Property { get; set; }
        public IqlLambdaExpression Expression { get; set; }
        public bool UseForFiltering { get; set; }

        public Mapping(IRelationshipDetail container)
        {
            Container = container;
        }
    }
}