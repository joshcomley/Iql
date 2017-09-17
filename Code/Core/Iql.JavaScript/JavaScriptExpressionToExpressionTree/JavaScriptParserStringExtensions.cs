namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public static class JavaScriptParserStringExtensions
    {
        public static string SubstringSafe(this string str, int start, int length)
        {
            if (start >= str.Length - 1)
            {
                return "";
            }
            if (start + length > str.Length - 1)
            {
                return str.Substring(start);
            }
            return str.Substring(start, length);
        }
    }
}