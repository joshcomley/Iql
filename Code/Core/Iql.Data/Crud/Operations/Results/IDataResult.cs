using System.Collections;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IDataResult
    {
        int? TotalCount { get; set; }
        IList Root { get; set; }
    }
}