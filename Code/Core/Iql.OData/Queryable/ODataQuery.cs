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
        private Dictionary<ODataQueryPart, List<string>> QueryParts { get; }
            = new Dictionary<ODataQueryPart, List<string>>();

        public ODataQuery(global::Iql.Queryable.IQueryable<T> queryable, IDataContext context)
        {
            Context = context;
        }

        public IDataContext Context { get; }

        public void AddQueryPart(ODataQueryPart queryPart, string part)
        {
            if (!QueryParts.ContainsKey(queryPart))
            {
                QueryParts.Add(queryPart, new List<string>());
            }
            QueryParts[queryPart].Add(part);
        }

        public List<string> GetQueryPart(ODataQueryPart queryPart)
        {
            if (QueryParts.ContainsKey(queryPart))
            {
                return QueryParts[queryPart];
            }
            return new List<string>();
        }

        public string ToODataQuery(bool isNested = false)
        {
            var query = "";
            var queryParts = new List<string>();
            if (HasKey)
            {
                query += "(" + Key + ")";
            }
            var filters = GetQueryPart(ODataQueryPart.Filter);
            if (filters.Count > 0)
            {
                queryParts.Add("$filter=" + string.Join(" and ", filters));
            }
            var orderBys = GetQueryPart(ODataQueryPart.OrderBy);
            if (orderBys.Count > 0)
            {
                queryParts.Add("$orderby=" + string.Join(",", orderBys));
            }
            var expands = GetQueryPart(ODataQueryPart.Expand);
            if (expands.Count > 0)
            {
                queryParts.Add("$expand=" + string.Join(",", expands));
            }
            var skip = GetQueryPart(ODataQueryPart.Skip);
            if (skip.Count > 0)
            {
                queryParts.Add("$skip=" + skip.Last());
            }
            var take = GetQueryPart(ODataQueryPart.Take);
            if (take.Count > 0)
            {
                queryParts.Add("$top=" + take.Last());
            }
            if (queryParts.Any())
            {
                if (!isNested)
                {
                    query += "?";
                }
                query += string.Join(isNested ? ";" : "&", queryParts);
            }
            return query;
        }

        IList IQueryResultBase.ToList()
        {
            return ToList();
        }

        public bool HasKey { get; set; }
        public object Key { get; set; }

        public override List<T> ToList()
        {
            throw new NotImplementedException();
        }
    }
}