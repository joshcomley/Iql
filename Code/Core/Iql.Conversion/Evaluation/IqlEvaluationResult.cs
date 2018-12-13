namespace Iql.Parsing.Evaluation
{
    public class IqlEvaluationResult<T>
    {
        public bool Success { get; }
        public T Result { get; }

        public IqlEvaluationResult(bool success, T result)
        {
            Success = success;
            Result = result;
        }

    }
}