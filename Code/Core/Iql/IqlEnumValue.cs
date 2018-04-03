namespace Iql
{
    public class IqlEnumValue
    {
        public string Name { get; set; }
        public long Value { get; set; }

        public IqlEnumValue(long value, string name)
        {
            Name = name;
            Value = value;
        }

#if !TypeScript
        public IqlEnumValue()
        {
            
        }
#endif
    }
}