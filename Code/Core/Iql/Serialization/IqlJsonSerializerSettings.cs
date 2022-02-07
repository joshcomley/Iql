namespace Iql.Serialization
{
    public class IqlJsonSerializerSettings
    {
        public bool IgnoreNulls { get; set; }
#if !TypeScript
        public bool UseNumberConverter { get; set; }
#endif
    }
}