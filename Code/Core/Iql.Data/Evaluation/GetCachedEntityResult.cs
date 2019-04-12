namespace Iql.Data.Evaluation
{
    public class GetCachedEntityResult
    {
        public bool Exists { get; }
        public object Entity { get; }

        public GetCachedEntityResult(bool exists, object entity)
        {
            Exists = exists;
            Entity = entity;
        }
    }
}