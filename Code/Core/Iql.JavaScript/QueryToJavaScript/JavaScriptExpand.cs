using Iql.Queryable.Operations;

namespace Iql.JavaScript.QueryToJavaScript
{
    public class JavaScriptExpand
    {
        public JavaScriptExpand(IExpandOperation operation)
        {
            Operation = operation;
        }

        public IExpandOperation Operation { get; }
    }
}