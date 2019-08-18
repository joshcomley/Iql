using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public interface ICrudResult
    {
        ICrudOperation Operation { get; }
        RequestStatus RequestStatus { get; set; }
        bool Success { get; set; }
    }
}