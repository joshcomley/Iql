using System;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable.Data.Lists;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityKey<T, TKey> : IEntityKey
    {
        public EntityKey()
        {
            Type = typeof(T);
            KeyType = typeof(TKey);
            Properties = new List<IProperty>();
        }

        public bool IsGeneratedRemotely { get; set; }
        public Type Type { get; set; }
        public bool HasRelationshipKeys => Properties.Any(p =>
            p.Relationship != null && !p.Relationship.ThisIsTarget);
        public Type KeyType { get; set; }
        public IList<IProperty> Properties { get; set; }
    }
}