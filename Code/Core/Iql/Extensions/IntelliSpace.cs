using System.Text;

namespace Iql.Extensions
{
    public static class StringExtensions
    {
        public static bool IsUpperCase(this char c)
        {
            return c.ToString().ToUpper() == c.ToString();
        }

        public static bool IsAlpha(this char c)
        {
            return (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z');
        }

        public static bool IsNumeric(this char c)
        {
            return c >= '0' && c <= '9';
        }
    }

    public class IntelliSpace
    {
        /// <summary>
        /// Takes a string like: "SomeWords" and converts it to "Some Words"
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Parse(string str)
        {
            if (str == null)
            {
                return null;
            }

            if (str.ToUpper() == str)
            {
                return str;
            }
            str = str.Replace('_', ' ');
            var sb = new StringBuilder();
            for (var i = 0; i < str.Length; i++)
            {
                // if it's upper case, inject a space
                if (i > 0)
                {
                    if (str[i].IsUpperCase() && str[i].IsAlpha())
                    {
                        // only if the last letter WAS NOT uppercase
                        if (!str[i - 1].IsUpperCase() && (str[i - 1].IsAlpha() || str[i - 1].IsNumeric()))
                        {
                            sb.Append(' ');
                            sb.Append(str[i]);
                        }
                        // or only if it is not the last letter and the next letter IS NOT uppercase
                        else if (i != str.Length - 1 && !str[i + 1].IsUpperCase() && (str[i - 1].IsAlpha() || str[i - 1].IsNumeric()))
                        {
                            sb.Append(' ');
                            sb.Append(str[i]);
                        }
                        else
                        {
                            sb.Append(str[i]);
                        }
                    }
                    else if (str[i].IsNumeric() && str[i - 1].IsAlpha())
                    {
                        sb.Append(' ');
                        sb.Append(str[i]);
                    }
                    else if (
                        i != str.Length - 1 &&
                        !str[i].IsAlpha() && !str[i].IsNumeric() && str[i] != ' ' &&
                        (str[i + 1].IsAlpha() || str[i + 1].IsNumeric() || str[i + 1] == ' '))
                    {
                        sb.Append(' ');
                        sb.Append(str[i]);
                    }
                    else
                    {
                        sb.Append(str[i]);
                    }
                }
                else
                {
                    sb.Append(str[i]);
                }
            }

            return sb.ToString();
        }
    }
}