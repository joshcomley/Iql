using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScriptExpression.Parsers
{
    public class JavaScriptOutput : IParserOutput
    {
        public string RootEntityParameterName { get; }
        private readonly string _code;

        public JavaScriptOutput(string code, string rootEntityParameterName)
        {
            RootEntityParameterName = rootEntityParameterName;
            _code = code;
        }

        public string ToCodeString()
        {
            return _code;
        }
    }
}