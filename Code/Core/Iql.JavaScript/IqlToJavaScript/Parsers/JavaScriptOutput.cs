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

        public string ToCSharpString()
        {
            return _code;
        }
    }
}