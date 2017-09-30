namespace Iql.Queryable.Operations
{
    public class TakeOperation : QueryOperation
    {
        public int Take { get; }

        public TakeOperation(int take)
        {
            Take = take;
        }
    }
}