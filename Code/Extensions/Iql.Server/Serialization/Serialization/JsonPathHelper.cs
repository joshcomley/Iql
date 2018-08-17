using System.Text.RegularExpressions;

namespace Iql.Server.Serialization.Serialization
{
    public class JsonPathHelper
    {
        public static bool IsEntityConfigurationProperty(string path, bool allowNested, params string[] properties)
        {
            var property = properties.Length > 1 ? $"({string.Join("|", properties)})" : properties[0];
            return Regex.IsMatch(path,
                $@"^{nameof(EntityConfigurationDocument.EntityTypes)}\[[0-9]+\]\.{property}(|\[[0-9]+\]){(allowNested ? "" : "$")}");
        }
    }
}