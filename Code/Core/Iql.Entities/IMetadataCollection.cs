using System.Collections.Generic;

namespace Iql.Entities
{
    public abstract class MetadataCollectionBase
    {
        protected readonly Dictionary<string, object> Dictionary = new Dictionary<string, object>();

        public IMetadataCollection Clear()
        {
            Dictionary.Clear();
            return this as IMetadataCollection;
        }

        public IMetadataCollection Set(string key, object value)
        {
            if (!Dictionary.ContainsKey(key))
            {
                Dictionary.Add(key, value);
            }
            else
            {
                Dictionary[key] = value;
            }

            return this as IMetadataCollection;
        }

        public IMetadataCollection Remove(string key)
        {
            if (Dictionary.ContainsKey(key))
            {
                Dictionary.Remove(key);
            }

            return this as IMetadataCollection;
        }

        public object Get(string key)
        {
            if (Has(key))
            {
                return Dictionary[key];
            }

            return null;
        }

        public bool Has(string key)
        {
            return Dictionary.ContainsKey(key);
        }

        public T GetAs<T>(string key)
        {
            return (T)Get(key);
        }
    }

    public class MetadataCollection : MetadataCollectionBase, IMetadataCollection
    {
        public KeyValuePair<string, object>[] All
        {
            get
            {
                var pairs = new List<KeyValuePair<string, object>>();
                foreach (var kvp in Dictionary)
                {
                    pairs.Add(kvp);
                }
                return pairs.ToArray();
            }
        }

    }
    public interface IMetadataCollection
    {
        KeyValuePair<string, object>[] All { get; }
        IMetadataCollection Clear();
        IMetadataCollection Set(string key, object value);
        IMetadataCollection Remove(string key);
        object Get(string key);
        bool Has(string key);
        T GetAs<T>(string key);
    }
}