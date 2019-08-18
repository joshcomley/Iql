namespace Iql.Data.Crud.Operations.Results
{
    public class CountDataResult<T> : CrudResultBase
        where T : class
    {
        public long? Count { get; set; }
        public bool IsOffline { get; set; }
        public GetDataOperation<T> Operation { get; }

        public CountDataResult(bool isOffline, GetDataOperation<T> operation, long? count, bool success) : base(operation, success)
        {
            IsOffline = isOffline;
            Operation = operation;
            Count = count;
        }
    }
}