namespace Iql.Entities.Functions
{
    public class IqlMethodArgument
    {
        public string Name { get; set; }
        public object Value { get; set; }

        public IqlMethodArgument(string name = null, object value = null)
        {
            Name = name;
            Value = value;
        }
    }
}