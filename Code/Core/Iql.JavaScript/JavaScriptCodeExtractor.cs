using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.JavaScript
{
    public static class JavaScriptCodeExtractor
    {
        private static readonly char[] Quotes = { '\'', '`', '"' };
        public static string RemoveComments(string code)
        {
            if (code == null)
            {
                throw new ArgumentException("\"code\" cannot be null.");
            }
            var inQuote = false;
            var inComment = false;
            var startIndex = 0;
            var quoteChar = '_';
            var lastChar = '_';
            for (var i = 0; i < code.Length; i++)
            {
                var startQuote = !inQuote && Quotes.Contains(code[i]);
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
                    if (!inComment && code.StartsWithAt(i, "//"))
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
            if (source == null)
            {
                return false;
            }
            if (str == null)
            {
                throw new ArgumentException("Value cannot be null.");
            }
            if (index + str.Length > source.Length)
            {
                return false;
            }
            return source.Substring(index, str.Length) == str;
        }

        private static readonly string[] ValidStarts = { "function ", "function(", "function\t" };
        private static readonly char[] Alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_".ToCharArray();
        private static readonly char[] Numeric = "01234567890".ToCharArray();
        private static readonly char[] AlphaNumeric = Alpha.Concat(Numeric).ToArray();
        private static readonly char[] Syntax = "(),".ToCharArray();
        private static readonly char[] Whitespace = " \t".ToCharArray();
        private static readonly char[] AllSyntax = Syntax.Concat(Whitespace).ToArray();

        public static JavaScriptFunctionBody ExtractBody(string code, bool isLambda = true)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            var copy = code;
            var commentsRemoved = RemoveComments(copy);
            return null;
            copy = commentsRemoved;
            // Remove whitespace
            copy = copy.Trim();
            var isWrapped = false;
            for (var k = 0; k < ValidStarts.Length; k++)
            {
                isWrapped = copy.StartsWith(ValidStarts[k]);
                if (isWrapped)
                {
                    break;
                }
            }
            var valid = Alpha;
            if (!isWrapped)
            {
                valid = Alpha.Concat(Syntax).ToArray();
                var j = 0;
                var variableBegun = false;
                var hasOpenBracket = false;
                while (valid.Contains(copy[j]))
                {
                    if (j == 0)
                    {
                        valid = Syntax.Concat(Whitespace).Concat(AlphaNumeric).ToArray();
                        if (copy[j] == '(')
                        {
                            hasOpenBracket = true;
                        }
                    }
                    if (!variableBegun)
                    {
                        if (Alpha.Contains(copy[j]))
                        {
                            variableBegun = true;
                        }
                        else if (!AllSyntax.Contains(copy[j]))
                        {
                            return null;
                        }
                    }
                    else if (!AlphaNumeric.Contains(copy[j]))
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
                var j = 0;
                var variableBegun = false;
                var isValidEs6 = false;
                while (j < copy.Length - 1)
                {
                    if (variableBegun)
                    {
                        if (!AlphaNumeric.Contains(copy[j]))
                        {
                            var whitespaceCount = 0;
                            while (Whitespace.Contains(copy[j]))
                            {
                                whitespaceCount++;
                                j++;
                            }
                            if (whitespaceCount == 0 || !copy.StartsWithAt(j, "=>"))
                            {
                                return null;
                            }
                            isValidEs6 = true;
                            break;
                        }
                    }
                    else if (Alpha.Contains(copy[j]))
                    {
                        variableBegun = true;
                    }
                    j++;
                }
                if (!isValidEs6)
                {
                    return null;
                }
            }
            // Remove function name (in ES6 we shouldn't have the function name)
            if (copy.StartsWith("function"))
            {
                copy = copy.Substring("function".Length);
            }

            // Remove whitespace
            copy = copy.Trim();
            // Remove (
            if (copy.StartsWith("("))
            {
                copy = copy.Substring(1);
                copy = copy.Trim();
            }

            var i = 0;
            valid = Alpha;
            var parameterNames = new List<string>();
            var validParameterSyntax = Whitespace.Concat(new[] { ',' }).ToArray();
            var signature = "";
            while (true)
            {
                var parameterName = "";
                while (valid.Contains(copy[i]))
                {
                    if (i == 0)
                    {
                        valid = Alpha.Concat(Numeric).ToArray();
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
            copy = copy.Trim();
            // Remove close bracket
            if (copy.StartsWith(")"))
            {
                copy = copy.Substring(1);
            }
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
            if (isLambda && copy.StartsWith("return"))
            {
                copy = copy.Substring("return".Length);
            }
            // Remove whitespace
            copy = copy.Trim();
            if (isLambda && copy.EndsWith(";"))
            {
                // Remove ;
                copy = copy.Substring(0, copy.Length - 1);
            }
            // Remove whitespace
            copy = copy.Trim();
            if (signature.StartsWith("(") && signature.EndsWith(")"))
            {
                signature = signature.Substring(1, signature.Length - 2);
            }
            signature = signature.Trim();
            return new JavaScriptFunctionBody(
                parameterNames.ToArray(),
                copy,
                signature,
                code.Trim(),
                $"function ({signature}) {{ {(isLambda ? "return " : "")}{copy}{(copy.EndsWith(";") ? "" : ";")} }}"
            );
        }
    }
}