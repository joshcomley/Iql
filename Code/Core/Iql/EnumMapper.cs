using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql
{
    public class EnumMapper<TEnum>
    {
        private readonly List<EnumMapping<TEnum>> _mapping = new List<EnumMapping<TEnum>>();

        public EnumMapper(params EnumMapping<TEnum>[] args)
        {
            _mapping = args.ToList();
        }

        private List<EnumMapping<TEnum>> GetMapping()
        {
            return _mapping;
        }

        public EnumMapper<TEnum> Map(Func<TEnum> value, string name = null)
        {
            _mapping.Add(new EnumMapping<TEnum>(value, name));
            return this;
        }

        public TEnum ResolveValue(object value)
        {
            return ResolveMapping(value).Value;
        }

        public string ResolveDescription(object value)
        {
            var mapping = ResolveMapping(value);
            return mapping.Description ?? mapping.ResolveName();
        }

        public string ResolveName(object value)
        {
            return ResolveMapping(value).ResolveName();
        }

        public EnumMapping<TEnum> ResolveMapping(object value)
        {
            if (value is string)
            {
                var number = 0;
                var success = int.TryParse(value.ToString(), out number);
                if (success)
                {
                    value = number;
                }
            }
            var mapping = GetMapping();
            if (value is string)
            {
                for (var i = 0; i < mapping.Count; i++)
                {
                    if (mapping[i].Description == (string) value)
                    {
                        return mapping[i];
                    }
                }
            }
            else
            {
                for (var i = 0; i < mapping.Count; i++)
                {
                    if (Equals(mapping[i].Value, value))
                    {
                        return mapping[i];
                    }
                }
            }
            throw new Exception("Unknown value: " + value);
        }
    }
}