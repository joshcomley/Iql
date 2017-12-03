using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Operations
{
    public class CompositeKey
    {
        public object Entity { get; set; }
        public List<KeyValue> Keys { get; } = new List<KeyValue>();

        public bool HasDefaultValue()
        {
            return Keys.Any(k => k.IsDefaultValue());
        }

        public bool Matches(CompositeKey compositeKey)
        {
            foreach (var key in compositeKey.Keys)
            {
                var matchedKey = Keys.SingleOrDefault(k => k.Name == key.Name);
                if (matchedKey == null)
                {
                    return false;
                }
                if (!Equals(matchedKey.Value, key.Value))
                {
                    return false;
                }
            }
            return true;
        }
    }
}