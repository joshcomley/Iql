using System;

namespace Iql.OData
{
    public class ODataParameter
    {
        public object Value { get; set; }
        public Type ValueType { get; set; }
        public string Name { get; set; }
        public bool IsKey { get; set; }

        public ODataParameter(object value, Type valueType, string name, bool isKey)
        {
            Value = value;
            ValueType = valueType;
            Name = name;
            IsKey = isKey;
        }
    }
}