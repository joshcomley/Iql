using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public interface ICrudResult
    {
        RequestStatus RequestStatus { get; set; }
        bool Success { get; set; }
    }
}