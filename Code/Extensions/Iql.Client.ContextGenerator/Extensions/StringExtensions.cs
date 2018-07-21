namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class StringExtensions
    {
        public static string FirstCharToLower(this string str)
        {
            return str.Substring(0, 1).ToLower() + str.Substring(1);
        }

        public static string FirstCharToUpper(this string str)
        {
            return str.Substring(0, 1).ToUpper() + str.Substring(1);
        }

        public static string AsTypeScriptTypeParameter(this string typeName)
        {
            switch (typeName)
            {
                case "boolean":
                case "string":
                case "number":
                    return typeName.FirstCharToUpper();
            }

            if (typeName.StartsWith("Array<"))
            {
                return "Array";
            }

            return typeName;
        }
    }
}