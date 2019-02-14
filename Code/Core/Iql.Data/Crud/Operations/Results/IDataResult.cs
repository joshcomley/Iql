using System.Collections;
using Iql.Data.Lists;
using Iql.Queryable;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IDataResult
    {
        int? TotalCount { get; set; }
        IList Root { get; set; }
        IQueryableBase Queryable { get; set; }
    }
}