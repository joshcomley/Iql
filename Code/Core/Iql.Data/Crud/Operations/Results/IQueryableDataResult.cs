using Iql.Data.Lists;
using Iql.Queryable;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IQueryableDataResult : IDataResult
    {
        IQueryableBase Queryable { get; set; }
    }
}