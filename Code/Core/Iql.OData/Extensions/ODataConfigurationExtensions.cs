using System;

namespace Iql.OData.Extensions
{
    public static class ODataConfigurationExtensions
    {
        public static string ResolveEntitySetUri<T>(this ODataConfiguration configuration)
        {
            return configuration.ResolveEntitySetUriByType(typeof(T));
        }

        public static string ResolveEntitySetUriByType(this ODataConfiguration configuration, Type type)
        {
            if (configuration == null)
            {
                return "";
            }
            var entitySetName = configuration.GetEntitySetNameByType(type);
            var apiUriBase = configuration.ApiUriBase;
            if (!apiUriBase.EndsWith("/"))
            {
                apiUriBase += "/";
            }
            var entitySetUri = $"{apiUriBase}{entitySetName}";
            return entitySetUri;
        }
    }
}