namespace Iql.Data.Extensions
{
    public static class StringExtensions
    {
        public static string PadLeft(this object obj, char c, int length)
        {
            var str = obj == null ? "" : obj.ToString();
            while (str.Length < length)
            {
                str = c + str;
            }
            return str;
        }
    }
}