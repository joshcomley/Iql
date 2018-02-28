//using Iql.Queryable;
//using Iql.Queryable.Data;
//using Iql.Queryable.Operations;
//using Iql.Queryable.Operations.Applicators;

//namespace Iql.OData.Queryable.Applicators
//{
//    public class ExpandCountOperationApplicatorOData : QueryOperationApplicator<IExpandCountOperation, IODataQuery, ODataQueryableAdapter>
//    {
//        public override void Apply<TEntity>(
//            IQueryOperationContext<IExpandCountOperation, TEntity, IODataQuery, ODataQueryableAdapter> context)
//        {
//            context.Data.AddQueryPart(ODataQueryPart.Expand,
//                ToExpandQuery(
//                    context,
//                    0));
//        }

//        private string ToExpandQuery<TEntity>(
//            IQueryOperationContext<IExpandCountOperation, TEntity, IODataQuery, ODataQueryableAdapter> context,
//            int index) 
//            where TEntity : class
//        {
//            if (index >= context.Operation.ExpandDetails.Count)
//            {
//                return "";
//            }
//            var detail = context.Operation.ExpandDetails[index];
//        }
//    }
//}