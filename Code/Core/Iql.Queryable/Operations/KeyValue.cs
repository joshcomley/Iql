namespace Iql.Queryable.Operations
{
    public class KeyValue
    {
        public string Name { get; }
        public object Value { get; }
        public KeyValue(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}