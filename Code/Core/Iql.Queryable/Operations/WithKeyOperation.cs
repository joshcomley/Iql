namespace Iql.Queryable.Operations
{
    public class WithKeyOperation : QueryOperation
    {
        public object Key { get; }

        public WithKeyOperation(object key)
        {
            Key = key;
        }
    }
}