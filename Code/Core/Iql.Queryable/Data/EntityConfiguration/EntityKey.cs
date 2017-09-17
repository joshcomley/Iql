using System;
using System.Collections.Generic;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public class EntityKey<T, TKey> : IEntityKey
    {
        public EntityKey()
        {
            Type = typeof(T);
            Properties = new List<IqlPropertyExpression>();
        }

        public Type Type { get; set; }

        public List<IqlPropertyExpression> Properties { get; set; }
    }
}