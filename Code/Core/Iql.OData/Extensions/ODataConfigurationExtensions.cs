using System;

namespace Iql.OData.Extensions
{
    public static class ODataConfigurationExtensions
    {
        public static string ResolveEntitySetUri<T>(this ODataConfiguration configuration)
        {
            return configuration.ResolveEntitySetUriByType(typeof(T));
        }

        public static string ResolveEntitySetUriByType(this ODataConfiguration configuration, Type type, string entitySetName = null)
        {
            if (configuration == null)
            {
                return "";
            }
            entitySetName = entitySetName ?? configuration.GetEntitySetNameByType(type);
            var apiUriBase = (configuration.ApiUriBase == null ? "" : configuration.ApiUriBase()) ?? "";
            if (!apiUriBase.EndsWith("/"))
            {
                apiUriBase += "/";
            }
            var entitySetUri = $"{apiUriBase}{entitySetName}";
            return entitySetUri;
        }
    }
}