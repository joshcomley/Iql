using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IFlattenedGetDataResultExtensions
    {
        public static bool IsSuccessful(this IFlattenedGetDataResult result)
        {
            return result.Success && result.Data != null;
        }
    }
}