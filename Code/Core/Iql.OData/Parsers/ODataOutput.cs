using Iql.Parsing;

namespace Iql.OData.Parsers
{
    public class ODataOutput : IParserOutput
    {
        private readonly string _code;

        public ODataOutput(string code)
        {
            _code = code;
        }

        public string ToCodeString()
        {
            return _code;
        }
    }
}