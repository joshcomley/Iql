namespace Iql.OData.Queryable
{
    class ODataUriPart
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public ODataUriPart(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}