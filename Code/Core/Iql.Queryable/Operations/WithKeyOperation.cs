using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;

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