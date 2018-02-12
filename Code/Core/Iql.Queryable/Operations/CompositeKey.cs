using System.Collections.Generic;
using System.Linq;

namespace Iql.Queryable.Operations
{
    public class CompositeKey
    {
        public static List<CompositeKey> All { get; set; }
        = new List<CompositeKey>();
        public CompositeKey(int size)
        {
            //All.Add(this);
            Keys = new KeyValue[size];
        }
        public object Entity { get; set; }
        public KeyValue[] Keys { get; }

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