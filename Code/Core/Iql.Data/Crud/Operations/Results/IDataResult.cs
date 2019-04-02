using System.Collections;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IDataResult
    {
        long? TotalCount { get; set; }
        IList Root { get; set; }
    }
}