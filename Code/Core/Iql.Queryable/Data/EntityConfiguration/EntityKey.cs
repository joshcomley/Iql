using System;
using System.Collections.Generic;

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
        public Type KeyType { get; set; }
        public List<IProperty> Properties { get; set; }
    }
}