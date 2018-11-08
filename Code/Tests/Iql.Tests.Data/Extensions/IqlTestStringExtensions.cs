namespace Iql.Tests.Data.Extensions
{
    public static class IqlTestStringExtensions
    {
        public static string NormaliseEncodedUri(this string uri)
        {
            return uri.Replace("%28", "(").Replace("%29", ")");
        }
    }
}