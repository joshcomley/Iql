using System.Collections.Generic;

namespace Iql.Data.Types
{
    public class TypeName
    {
        public string Name { get; set; }
        public string[] Generics { get; set; }

        public static TypeName Parse(string fullName)
        {
            var type = new TypeName();
            var isInBrackets = 0;
            var part = "";
            var genericParts = new List<string>();
            for (var i = 0; i < fullName.Length; i++)
            {
                if (fullName[i] == '<')
                {
                    if (isInBrackets == 0)
                    {
                        type.Name = part;
                        part = "";
                        isInBrackets++;
                        continue;
                    }
                    isInBrackets++;
                }
                else if (fullName[i] == '>')
                {
                    isInBrackets--;
                    if (isInBrackets == 0)
                    {
                        genericParts.Add(part.Trim());
                        part = "";
                        continue;
                    }
                }
                if (isInBrackets == 1 && fullName[i] == ',')
                {
                    genericParts.Add(part.Trim());
                    part = "";
                    continue;
                }
                part += fullName[i];
            }
            if (!string.IsNullOrWhiteSpace(part))
            {
                type.Name = part;
            }
            type.Generics = genericParts.ToArray();
            return type;
        }
    }
}