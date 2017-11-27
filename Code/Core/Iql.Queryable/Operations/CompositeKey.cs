using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Operations
{
    public class CompositeKey
    {
        public List<KeyValue> Keys { get; } = new List<KeyValue>();

        public bool HasDefaultValue()
        {
            return Keys.Any(k => k.IsDefaultValue());
        }
    }
}