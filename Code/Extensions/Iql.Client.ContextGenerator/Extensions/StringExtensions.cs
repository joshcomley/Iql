namespace Iql.OData.TypeScript.Generator.Extensions
{
    public static class StringExtensions
    {
        public static string StripNullability(this string str)
        {
            while (str.EndsWith("?"))
            {
                str = str.Substring(0, str.Length - 1);
            }

            return str;
        }
        public static string AsSafeClassName(this string str)
        {
            return str.Replace(".", "");
        }
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