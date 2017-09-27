using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Operations;

namespace Iql.OData.Queryable
{
    public class ODataQuery<T> : QueryResult<T>, IODataQuery
    {
        public ODataQuery(global::Iql.Queryable.IQueryable<T> queryable, IDataContext context)
        {
            Context = context;
        }

        public IDataContext Context { get; }

        public string ToODataQuery()
        {
            var query = "";
            var queryParts = new List<string>();
            if (HasKey)
            {
                query += "(" + Key + ")";
            }
            if (Filters.Count > 0)
            {
                queryParts.Add("$filter=" + string.Join(" and ", Filters));
            }
            if (OrderBys.Count > 0)
            {
                queryParts.Add("$orderby=" + string.Join(",", OrderBys));
            }
            if (Expands.Count > 0)
            {
                var expands = new List<string>();
                Expands.ForEach(expand =>
                {
                    //expands.Add(ODataQueryableAdapter.GetExpression(element));
                    expands.Add(ToExpandQuery(expand, 0));
                });
                queryParts.Add("$expand=" + string.Join(",", expands));
            }
            if (queryParts.Any())
            {
                query += "?";
                query += string.Join("&", queryParts);
            }
            return query;
        }

        IList IQueryResultBase.ToList()
        {
            return ToList();
        }

        public bool HasKey { get; set; }
        public object Key { get; set; }
        public List<string> Filters { get; set; } = new List<string>();
        public List<string> OrderBys { get; set; } = new List<string>();
        public List<IExpandOperation> Expands { get; set; } = new List<IExpandOperation>();

        private string ToExpandQuery(IExpandOperation expand, int index)
        {
            if (index >= expand.ExpandDetails.Count)
            {
                return "";
            }
            var detail = expand.ExpandDetails[index];
            var str = detail.Relationship.SourceProperty.PropertyName;
            str += "(";
            var nested = ToExpandQuery(expand, index + 1);
            if (!string.IsNullOrWhiteSpace(nested))
            {
                str += "$expand=";
                str += nested;
            }
            //        if (index === expand.ExpandDetails.Count - 1 && expand.queryExpression.queryable) {
            str += detail.TargetQueryable.ToQueryWithAdapter(new ODataQueryableAdapter(), Context).ToODataQuery();
            //        }
            str += ")";
            return str;
        }

        public override List<T> ToList()
        {
            throw new NotImplementedException();
        }
    }
}