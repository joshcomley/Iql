using System;
using System.Linq;
using Iql.Queryable.Data;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator.Applicators;

namespace Iql.OData.QueryableApplicator.Applicators
{
    public class WithKeyOperationApplicatorOData : QueryOperationApplicator<WithKeyOperation, IODataQuery, ODataQueryableAdapter>
    {
        public override void Apply<TEntity>(IQueryOperationContext<WithKeyOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
        {
            context.Data.Key = context.Operation.Key;
            context.Data.HasKey = true;
        }

        public static string FormatKey(CompositeKey key)
        {
            string keyString;
            if (key.Keys.Length == 1)
            {
                keyString = GetKeyValue(key.Keys.Single());
            }
            else
            {
                var keys = key.Keys.Select(k => k.Name + "=" + GetKeyValue(k));
                keyString = string.Join(",", keys);
            }
            return keyString;
        }

        private static string GetKeyValue(KeyValue key)
        {
            if (key.Value is string || key.Value is Guid || key.Value is Guid? || 
                (key.ValueType != null && 
                 (key.ValueType.Type == typeof(string) ||  key.ValueType.Type == typeof(Guid))))
            {
                return $"\'{key.Value}\'";
            }

            return key.Value.ToString();
        }
    }
}