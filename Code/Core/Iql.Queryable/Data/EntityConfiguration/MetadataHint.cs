namespace Iql.Queryable.Data.EntityConfiguration
{
    public class MetadataHint
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public MetadataHint(string name, string value = null)
        {
            Name = name;
            Value = value;
        }

        public string Formatted()
        {
            return Value == null 
                ? Name 
                : $"{Name}:{Value}";
        }
    }
}