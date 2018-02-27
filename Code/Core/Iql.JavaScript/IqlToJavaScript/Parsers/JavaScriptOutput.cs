using Iql.Parsing;

namespace Iql.JavaScript.IqlToJavaScript.Parsers
{
    public class JavaScriptOutput : IParserOutput
    {
        private readonly string _code;

        public JavaScriptOutput(string code)
        {
            _code = code;
        }

        public string ToCodeString()
        {
            return _code;
        }
    }
}