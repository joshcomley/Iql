namespace Iql.Data.Methods
{
    public class DataMethodResult<T> : MethodResult
    {
        public T Data { get; set; }

        public DataMethodResult(bool success) : base(success)
        {
        }
    }
}