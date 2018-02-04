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
            if (key.Keys.Count == 1)
            {
                ketStr = key.Keys.Single().Value.ToString();
            }
            else
            {
                var keys = key.Keys.Select(k => k.Name + "=" + k.Value);
                ketStr = string.Join(",", keys);
            }
            return ketStr;
        }
    }
}