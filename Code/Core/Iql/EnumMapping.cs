using System;

namespace Iql
{
    public class EnumMapping<TEnum>
    {
        private readonly Func<TEnum> _valueGetter;
        private string _name;

        public EnumMapping(Func<TEnum> valueGetter,
            string description)
        {
            Description = description;
            _valueGetter = valueGetter;
            Value = valueGetter();
        }

        public string Description { get; }
        public TEnum Value { get; set; }

        public string ResolveName()
        {
            if (string.IsNullOrWhiteSpace(_name))
            {
                var name = _valueGetter().ToString();
                return name;
                //name = name.Substring(name.LastIndexOf(".") + 1);
                //var semicolon = name.LastIndexOf(";");
                //name = name.Substring(0, semicolon);
                //_name = name;
            }
            return _name;
        }
    }
}