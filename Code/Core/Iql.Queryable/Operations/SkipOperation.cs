namespace Iql.Queryable.Operations
{
    public class SkipOperation : QueryOperation
    {
        public int Skip { get; }

        public SkipOperation(int skip)
        {
            Skip = skip;
        }
    }
}