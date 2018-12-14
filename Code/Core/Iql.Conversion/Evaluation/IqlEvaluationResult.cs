namespace Iql.Parsing.Evaluation
{
    public class IqlEvaluationResult<T>
    {
        public bool Success { get; set; }
        public T Result { get; set; }

        public IqlEvaluationResult(bool success, T result)
        {
            Success = success;
            Result = result;
        }

    }
}