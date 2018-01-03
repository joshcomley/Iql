using System;
using System.Linq;
using Iql.Queryable.Data;
using Iql.Queryable.Operations;
using Iql.Queryable.Operations.Applicators;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class OrderByOperationApplicatorJavaScript : QueryOperationApplicator<OrderByOperation,
        IJavaScriptQueryResult>
    {
        public override void Apply<TEntity>(
            IQueryOperationContext<OrderByOperation, TEntity, IJavaScriptQueryResult> context)
        {
            //var orderBy = JavaScriptQueryableAdapter.GetExpression(
            //    context.Operation,
            //    false,
            //    context.DataContext.EntityConfigurationContext, "");
            //var orderByExpression = new JavaScriptOrderByExpression(
            //    orderBy.RootVariableName,
            //    orderBy.Expression,
            //    context.Operation.IsDescending()
            //);
            context.Data.Query.AppendLine(
                ".sort(" + ResolveSort(context.Operation, 0, false) + ")");
        }

        private string ResolveSort(OrderByOperation orderByOperation, int index, bool lastDescending)
        {
            var OrderBys = new[] {orderByOperation}.ToList();
            if (index >= OrderBys.Count)
            {
                return null;
            }
            var filter = OrderBys[index];
            if (filter.IsDescending())
            {
                lastDescending = !lastDescending;
            }
            var sort = "this." + nameof(OrderBy) + "('" + filter.Expression + "', " +
                       (lastDescending ? "true" : "false") + ", null";
            if (index < OrderBys.Count - 1)
            {
                sort += ",";
                sort += ResolveSort(orderByOperation, ++index, lastDescending);
            }
            sort += ")";
            return sort;
        }

        protected Func<object, object, int> OrderBy(string path, bool reverse, Func<object, object> primer,
            Func<object, object, int> then = null)
        {
            Func<object, string, object> get = (obj, path_) =>
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    var pathParts = path_.Split('.');
                    var len = pathParts.Length - 1;
                    for (var i = 0; i < len; i++)
                    {
                        obj = obj.GetPropertyValueByName(pathParts[i]);
                    }
                    ;
                    return obj.GetPropertyValueByName(pathParts[len]);
                }
                return obj;
            };
            Func<object, object> prime = obj => { return primer != null ? primer(get(obj, path)) : get(obj, path); };

            Func<object, object, int> result = (a, b) =>
            {
                var A = prime(a) as IComparable;
                var B = prime(b) as IComparable;
                var compare = A.CompareTo(B);
                return (
                           compare != 0 ? compare : then?.Invoke(a, b) ?? 0
                       ) * (reverse ? -1 : 1);
            };
            return result;
        }
    }
}