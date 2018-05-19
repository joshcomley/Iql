namespace Iql.Data.Methods
{
    public class MethodResult
    {
        public bool Success { get; set; }

        public MethodResult(bool success)
        {
            Success = success;
        }
    }
}