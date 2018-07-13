using System.Collections.Generic;

namespace Iql.Entities.Enums
{
    public interface IEnumValue
    {
        string Name { get; set; }
        long Value { get; set; }
    }

    public interface IEnumConfiguration
    {
        bool IsFlags { get; set; }
        string Name { get; set; }
        List<IEnumValue> Values { get; set; }
        IEnumConfiguration DefineValue(string name, long value);
        IEnumConfiguration SetIsFlags(bool isFlags = true);
    }

    public class EnumValue : IEnumValue
    {
        public string Name { get; set; }
        public long Value { get; set; }

        public EnumValue(string name, long value)
        {
            Name = name;
            Value = value;
        }
    }

    public class EnumConfiguration : IEnumConfiguration
    {
        public string Name { get; set; }
        public bool IsFlags { get; set; }

        public EnumConfiguration(string name)
        {
            Name = name;
        }
        public List<IEnumValue> Values { get; set; } = new List<IEnumValue>();

        public IEnumConfiguration DefineValue(string name, long value)
        {
            Values.Add(new EnumValue(name, value));
            return this;
        }

        public IEnumConfiguration SetIsFlags(bool isFlags = true)
        {
            IsFlags = isFlags;
            return this;
        }
    }
}