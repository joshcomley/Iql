namespace Iql.Entities.Relationships
{
    public class Mapping<T> : IMapping<T>
    {
        public T Container { get; set; }
        public IqlExpression Expression { get; set; }
        public bool UseForFiltering { get; set; }
    }
}