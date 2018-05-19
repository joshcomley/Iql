using Iql.Entities;
using Iql.Queryable.Operations;

namespace Iql.Data.Operations
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