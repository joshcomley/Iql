using System;
using System.Collections.Generic;
using System.Linq;
using Iql.OData.QueryableApplicator.Applicators;
using Iql.Queryable;
using Iql.Queryable.Data;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Data.EntityConfiguration;
using Iql.Queryable.Operations;
using Iql.Queryable.QueryApplicator;

namespace Iql.OData.QueryableApplicator
{
    public class ODataQuery<T> : QueryResult<IODataQuery>, IODataQuery
    {
        private Dictionary<ODataQueryPart, List<string>> QueryParts { get; }
            = new Dictionary<ODataQueryPart, List<string>>();

        public ODataQuery(global::Iql.Queryable.Data.Queryable.IQueryable<T> queryable, IDataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IDataContext DataContext { get; }

        public int TotalSkip { get; set; }
        public int TotalTake { get; set; }

        public void AddQueryPart(ODataQueryPart queryPart, string part)
        {
            EnsureQueryPart(queryPart);
            QueryParts[queryPart].Add(part);
        }

        public void SetQueryPart(ODataQueryPart queryPart, string part)
        {
            EnsureQueryPart(queryPart);
            QueryParts[queryPart] = new List<string>(new []{ part });
        }

        private void EnsureQueryPart(ODataQueryPart queryPart)
        {
            if (!QueryParts.ContainsKey(queryPart))
            {
                QueryParts.Add(queryPart, new List<string>());
            }
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
            var queryParts = new List<ODataUriPart>();
            if (HasKey)
            {
                query += $"({WithKeyOperationApplicatorOData.FormatKey(Key)})";
            }
            var filters = GetQueryPart(ODataQueryPart.Filter);
            if (filters.Count > 0)
            {
                queryParts.Add(new ODataUriPart("$filter", string.Join(" and ", filters)));
            }
            var orderBys = GetQueryPart(ODataQueryPart.OrderBy);
            if (orderBys.Count > 0)
            {
                queryParts.Add(new ODataUriPart("$orderby", string.Join(",", orderBys)));
            }
            var expands = GetQueryPart(ODataQueryPart.Expand);
            if (expands.Count > 0)
            {
                queryParts.Add(new ODataUriPart("$expand", string.Join(",", expands)));
            }
            var skip = GetQueryPart(ODataQueryPart.Skip);
            if (skip.Count > 0)
            {
                queryParts.Add(new ODataUriPart("$skip", skip.Last()));
            }
            var take = GetQueryPart(ODataQueryPart.Take);
            if (take.Count > 0)
            {
                queryParts.Add(new ODataUriPart("$top", take.Last()));
            }
            if (IncludeCount)
            {
                queryParts.Add(new ODataUriPart("$count", "true"));
            }
            if (queryParts.Any())
            {
                if (!isNested)
                {
                    query += "?";
                }

              query += string.Join(
                isNested ? ";" : "&",
                queryParts.Select(
                  q => q.Name + "=" +
                       (q.Name == "$expand" ? q.Value : Uri.EscapeDataString(q.Value))));
            }
            return query;
        }

        public bool IncludeCount { get; set; }
        public bool HasKey { get; set; }
        public CompositeKey Key { get; set; }
    }
}