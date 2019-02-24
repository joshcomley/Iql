namespace Iql.Parsing.Extensions
{
    public static class IqlParsingObjectExtensions
    {
        public static T NullPropagate<T>(this object source, string propertyName)
        {
            if (source == null)
            {
                return default(T);
            }
            return (T)source.GetType().GetProperty(propertyName).GetValue(source);
        }
    }
}