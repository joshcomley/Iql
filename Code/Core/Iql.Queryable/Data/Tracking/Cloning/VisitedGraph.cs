using System.Collections.Generic;
using TypeSharp.Extensions;

namespace Iql.Queryable.Data.Tracking.Cloning
{
    [DoNotConvert]
    internal class VisitedGraph : Dictionary<object, object>
    {
        public new object this[object key]
        {
            get
            {
                if (key == null)
                {
                    return null;
                }
                return base[key];
            }
        }

        public new bool ContainsKey(object key)
        {
            if (key == null)
            {
                return true;
            }
            return base.ContainsKey(key);
        }
    }
}