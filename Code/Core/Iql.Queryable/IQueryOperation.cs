namespace Iql.Queryable
{
    public interface IQueryOperation
    {
#if TypeScript
        EvaluateContext EvaluateContext { get; set; }
#endif
    }
}