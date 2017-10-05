namespace Iql.Queryable.Operations
{
    public class WithKeyOperation : QueryOperation
    {
        public CompositeKey Key { get; }

        public WithKeyOperation(CompositeKey key)
        {
            Key = key;
        }
    }
}