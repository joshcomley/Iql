namespace Iql.OData.TypeScript.Generator.Parsers
{
    public class ODataSchemaParser
    {
        public ODataSchemaParser(string odataXml, string iqlJson)
        {
            OdataXml = odataXml;
            IqlJson = iqlJson;
        }

        public string OdataXml { get; set; }
        public string IqlJson { get; }

        public ODataSchema Parse()
        {
            var parser = new ODataSchemaParserInternal();
            return parser.Parse(OdataXml, IqlJson);
        }
    }
}