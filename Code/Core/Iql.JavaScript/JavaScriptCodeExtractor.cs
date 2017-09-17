using System.Collections.Generic;
using System.Linq;

namespace Iql.JavaScript
{
    public static class JavaScriptCodeExtractor
    {
        public static string RemoveComments(string code)
        {
            var quotes = new[] {'\'', '`', '"'};
            var inQuote = false;
            var inComment = false;
            var startIndex = 0;
            var quoteChar = '_';
            var lastChar = '_';
            for (var i = 0; i < code.Length; i++)
            {
                var startQuote = !inQuote && quotes.Contains(code[i]);
                var endQuote = inQuote && code[i] == quoteChar;
                if (!inComment && (startQuote || endQuote))
                {
                    if (endQuote && lastChar != '\\')
                    {
                        inQuote = false;
                        quoteChar = '_';
                    }
                    else if (startQuote)
                    {
                        inQuote = true;
                        quoteChar = code[i];
                    }
                }
                if (!inQuote)
                {
                    int endIndex;
                    if (code.StartsWithAt(i, "//"))
                    {
                        startIndex = i;
                        while (i < code.Length && code[i] != '\n')
                        {
                            i++;
                        }
                        endIndex = i;
                        i = 0;
                        code = code.Substring(0, startIndex) + code.Substring(endIndex, code.Length - endIndex);
                    }
                    if (!inComment && code.StartsWithAt(i, "/*"))
                    {
                        startIndex = i;
                        inComment = true;
                    }
                    if (inComment && code.StartsWithAt(i, "*/"))
                    {
                        endIndex = i + 2;
                        inComment = false;
                        i = 0;
                        code = code.Substring(0, startIndex) + code.Substring(endIndex, code.Length - endIndex);
                    }
                }
                lastChar = code[i];
            }
            return code;
        }

        private static bool StartsWithAt(this string source, int index, string str)
        {
            if (index + str.Length > source.Length)
            {
                return false;
            }
            return source.Substring(index, str.Length) == str;
        }

        public static JavaScriptFunctionBody ExtractBody(string code)
        {
            var copy = code;
            var commentsRemoved = RemoveComments(copy);
            copy = commentsRemoved;
            // Remove whitespace
            copy = copy.Trim();
            var validStarts = new[] {"function ", "function(", "function\t"};
            var isWrapped = false;
            for (var k = 0; k < validStarts.Length; k++)
            {
                isWrapped = copy.StartsWith(validStarts[k]);
                if (isWrapped)
                {
                    break;
                }
            }
            var alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_".ToCharArray();
            var numeric = "01234567890".ToCharArray();
            var alphaNumeric = alpha.Concat(numeric).ToArray();
            var syntax = "(),".ToCharArray();
            var whitespace = " \t".ToCharArray();
            var allSyntax = syntax.Concat(whitespace).ToArray();
            var valid = alpha;
            if (!isWrapped)
            {
                valid = alpha.Concat(syntax).ToArray();
                var j = 0;
                var variableBegun = false;
                var hasOpenBracket = false;
                while (valid.Contains(copy[j]))
                {
                    if (j == 0)
                    {
                        valid = syntax.Concat(whitespace).Concat(alphaNumeric).ToArray();
                        if (copy[j] == '(')
                        {
                            hasOpenBracket = true;
                        }
                    }
                    if (!variableBegun)
                    {
                        if (alpha.Contains(copy[j]))
                        {
                            variableBegun = true;
                        }
                        else if (!allSyntax.Contains(copy[j]))
                        {
                            return null;
                        }
                    }
                    else if (!alphaNumeric.Contains(copy[j]))
                    {
                        variableBegun = false;
                    }
                    if (hasOpenBracket && copy[j] == ')')
                    {
                        var copyOfCopy = copy.Substring(j + 1).Trim();
                        if (!copyOfCopy.StartsWith("=>"))
                        {
                            return null;
                        }
                        isWrapped = true;
                        break;
                    }
                    if (!variableBegun)
                    {
                        if (copy.StartsWithAt(j, "=>"))
                        {
                            isWrapped = true;
                            break;
                        }
                    }
                    j++;
                }
            }
            if (!isWrapped)
            {
                return null;
            }
            // Remove function name (in ES6 we shouldn't have the function name)
            if (copy.StartsWith("function"))
            {
                copy = copy.Substring("function".Length);
            }

            // Remove whitespace
            copy = copy.Trim();
            // Remove (
            copy = copy.Substring(1);
            copy = copy.Trim();

            var i = 0;
            valid = alpha;
            var parameterNames = new List<string>();
            var validParameterSyntax = whitespace.Concat(new[] {','}).ToArray();
            var signature = "";
            while (true)
            {
                var parameterName = "";
                while (valid.Contains(copy[i]))
                {
                    if (i == 0)
                    {
                        valid = alpha.Concat(numeric).ToArray();
                    }
                    parameterName += copy[i];
                    signature += copy[i];
                    i++;
                }
                parameterNames.Add(parameterName);
                if (copy.StartsWithAt(i, ")") || copy.StartsWithAt(i, "=>"))
                {
                    break;
                }
                var finished = false;
                while (validParameterSyntax.Contains(copy[i]))
                {
                    signature += copy[i];
                    i++;
                    if (copy.StartsWithAt(i, ")") || copy.StartsWithAt(i, "=>"))
                    {
                        finished = true;
                        break;
                    }
                }
                if (finished)
                {
                    break;
                }
            }
            // Remove parameters
            copy = copy.Substring(i);
            copy = copy.Substring(copy.IndexOf(")") + 1);
            copy = copy.Trim();
            // If ES6
            if (copy.StartsWith("=>"))
            {
                copy = copy.Substring(2);
            }
            else
            {
                // Remove { ... } brackets
                copy = copy.Substring(1, copy.Length - 2);
            }
            // Remove whitespace
            copy = copy.Trim();
            // Remove "return" (shouldn't be there in ES6 syntax but we'll allow it)
            if (copy.StartsWith("return"))
            {
                copy = copy.Substring("return".Length);
            }
            // Remove whitespace
            copy = copy.Trim();
            if (copy.EndsWith(";"))
            {
                // Remove ;
                copy = copy.Substring(0, copy.Length - 1);
            }
            // Remove whitespace
            copy = copy.Trim();
            return new JavaScriptFunctionBody(
                parameterNames.ToArray(),
                copy,
                signature,
                code,
                commentsRemoved
            );
        }
    }
}