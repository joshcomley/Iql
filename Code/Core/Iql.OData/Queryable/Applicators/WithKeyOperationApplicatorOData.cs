using System;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;
using System.Linq;

namespace Iql.OData.Queryable.Applicators
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
            string ketStr;
            if (key.Keys.Length == 1)
            {
                ketStr = GetKeyValue(key.Keys.Single());
            }
            else
            {
                var keys = key.Keys.Select(k => k.Name + "=" + GetKeyValue(k));
                ketStr = string.Join(",", keys);
            }
            return ketStr;
        }

        private static string GetKeyValue(KeyValue key)
        {
            if (key.Value is string || key.Value is Guid || key.Value is Guid? || 
                (key.ValueType != null && 
                 (key.ValueType == typeof(string) ||  key.ValueType == typeof(Guid))))
            {
                return $"\'{key.Value}\'";
            }

            return key.Value.ToString();
        }
    }
}