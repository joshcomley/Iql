using System;
using System.Collections.Generic;
using System.Linq;

namespace Iql.JavaScript
{
    public static class JavaScriptCodeExtractor
    {
        private const char SingleQuote = '\'';
        private const char TildeQuote = '`';
        private const char DoubleQuote = '"';
        public static string RemoveComments(string codeStr)
        {
            if (codeStr == null)
            {
                throw new ArgumentException("\"code\" cannot be null.");
            }
            if (codeStr.IndexOf(@"//") == -1 && codeStr.IndexOf(@"/*") == -1)
            {
                return codeStr;
            }
            var code = codeStr.ToCharArray();
            var inQuote = false;
            var inComment = false;
            var startIndex = 0;
            var lastStartIndex = 0;
            var currentQuoteChar = '_';
            var lastChar = '_';
            var codeChunks = new List<int[]>();
            for (var i = 0; i < code.Length; i++)
            {
                var ch = code[i];
                var startQuote = !inQuote &&
                                 (ch == SingleQuote || ch == DoubleQuote || ch == TildeQuote);// && Quotes.Contains(code[i]);
                var endQuote = inQuote && ch == currentQuoteChar;
                if (!inComment && (startQuote || endQuote))
                {
                    if (endQuote && lastChar != '\\')
                    {
                        inQuote = false;
                        currentQuoteChar = '_';
                    }
                    else if (startQuote)
                    {
                        inQuote = true;
                        currentQuoteChar = ch;
                    }
                }
                if (!inQuote)
                {
                    int endIndex;
                    if (!inComment && ch == '/' && i < code.Length - 1 && code[i + 1] == '/')
                    {
                        startIndex = i;
                        while (i < code.Length && code[i] != '\n')
                        {
                            i++;
                        }
                        endIndex = i;
                        //i = 0;
                        codeChunks.Add(new[] { lastStartIndex, startIndex - lastStartIndex });
                        lastStartIndex = endIndex;
                        //code = 
                        //    (codeStr.Substring(0, startIndex) + codeStr.Substring(endIndex, code.Length - endIndex)).ToCharArray();
                    }
                    if (!inComment && ch == '/' && i < code.Length - 1 && code[i + 1] == '*')
                    {
                        startIndex = i;
                        inComment = true;
                    }
                    if (inComment && ch == '*' && i < code.Length - 1 && code[i + 1] == '/')
                    {
                        endIndex = i + 2;
                        inComment = false;
                        //i = 0;
                        //code = (codeStr.Substring(0, startIndex) + codeStr.Substring(endIndex, code.Length - endIndex)).ToCharArray();
                        codeChunks.Add(new[] { lastStartIndex, startIndex - lastStartIndex });
                        lastStartIndex = endIndex;
                    }
                }
                lastChar = ch;
            }

            var result = "";
            foreach (var chunk in codeChunks)
            {
                result += codeStr.Substring(chunk[0], chunk[1]);
            }
            return result;
        }

        //private static readonly string[] ValidStarts = { "function ", "function(", "function\t" };
        private static readonly char[] Alpha = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_".ToCharArray();
        private static readonly char[] Numeric = "01234567890".ToCharArray();
        private static readonly char[] AlphaNumeric = Alpha.Concat(Numeric).ToArray();
        private static readonly char[] Syntax = "(),".ToCharArray();
        private static readonly char[] Whitespace = " \t".ToCharArray();
        private static readonly char[] AllSyntax = Syntax.Concat(Whitespace).ToArray();
        private static readonly char[] AlphaAndSyntax = Alpha.Concat(Syntax).ToArray();
        private static readonly char[] SyntaxAndWhitespaceAndAlphaNumeric = Syntax.Concat(Whitespace).Concat(AlphaNumeric).ToArray();

        public static JavaScriptFunctionBody ExtractBodyOld(string code, bool isLambda = true)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            var copy = RemoveComments(code);
            // Remove whitespace
            copy = copy.Trim();
            var isWrapped =
                copy.StartsWith("function ")
                ||
                copy.StartsWith("function(")
                ||
                copy.StartsWith("function\t");

            char[] valid;
            if (!isWrapped)
            {
                valid = AlphaAndSyntax;
                var j = 0;
                var ch = copy[j];
                var variableBegun = false;
                var hasOpenBracket = false;
                while (valid.Contains(ch))
                {
                    if (j == 0)
                    {
                        valid = SyntaxAndWhitespaceAndAlphaNumeric;
                        if (ch == '(')
                        {
                            hasOpenBracket = true;
                        }
                    }
                    if (!variableBegun)
                    {
                        if (Alpha.Contains(ch))
                        {
                            variableBegun = true;
                        }
                        else if (!AllSyntax.Contains(ch))
                        {
                            return null;
                        }
                    }
                    else if (!AlphaNumeric.Contains(ch))
                    {
                        variableBegun = false;
                    }
                    if (hasOpenBracket && ch == ')')
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
                        if (ch == '=' && j < copy.Length - 1 && copy[j + 1] == '>')
                        {
                            isWrapped = true;
                            break;
                        }
                    }
                    j++;
                    ch = copy[j];
                }
            }
            if (!isWrapped)
            {
                var j = 0;
                var ch = copy[j];
                var variableBegun = false;
                var isValidEs6 = false;
                while (j < copy.Length - 1)
                {
                    if (variableBegun)
                    {
                        if (!AlphaNumeric.Contains(ch))
                        {
                            var whitespaceCount = 0;
                            while (Whitespace.Contains(ch))
                            {
                                whitespaceCount++;
                                j++;
                                ch = copy[j];
                            }
                            if (whitespaceCount == 0 ||
                                ch != '=' || j >= copy.Length - 1
                                || copy[j + 1] != '>'
                                )
                            {
                                return null;
                            }
                            isValidEs6 = true;
                            break;
                        }
                    }
                    else if (Alpha.Contains(ch))
                    {
                        variableBegun = true;
                    }
                    j++;
                    ch = copy[j];
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
                var ch = copy[i];
                while (valid.Contains(ch))
                {
                    if (i == 0)
                    {
                        valid = Alpha.Concat(Numeric).ToArray();
                    }
                    parameterName += ch;
                    signature += ch;
                    i++;
                    ch = copy[i];
                }
                parameterNames.Add(parameterName);
                if (ch == ')' ||
                    (ch == '=' && i < copy.Length - 1 && copy[i + 1] == '>'))
                {
                    break;
                }
                var finished = false;
                while (validParameterSyntax.Contains(ch))
                {
                    signature += ch;
                    i++;
                    ch = copy[i];
                    if (ch == ')' ||
                        (ch == '=' && i < copy.Length - 1 && copy[i + 1] == '>'))
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

        public static JavaScriptFunctionBody ExtractBody(string code, bool isLambda = true)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
            var copy = RemoveComments(code);
            // Remove whitespace
            copy = copy.Trim();
            var isEs5 =
                copy.StartsWith("function ")
                ||
                copy.StartsWith("function(")
                ||
                copy.StartsWith("function\t");

            if (isEs5)
            {
                return ExtractEs5Body(code);
            }
            return ExtractEs6Body(code);
        }

        private static JavaScriptFunctionBody ExtractEs6Body(string code)
        {
            var original = code;
            var lambdaIndex = code.IndexOf("=>");
            var signature = code.Substring(0, lambdaIndex);
            if (signature.StartsWith("("))
            {
                signature = signature.Substring(1, signature.Length - 2);
            }
            signature = signature.Trim();
            var parameterNames = signature.Split(',').ToArray();
            code = code.Substring(lambdaIndex + 2).Trim();
            return new JavaScriptFunctionBody(
                parameterNames,
                code.Trim(';'),
                signature,
                original,
                $"function ({signature}) {{ return {code}; }}");
        }

        private static JavaScriptFunctionBody ExtractEs5Body(string code)
        {
            var original = code;
            code = code.Substring("function".Length);
            code = code.Trim();
            code = code.Substring(1);
            var closeBracket = code.IndexOf(')');
            var signature = code.Substring(0, closeBracket);
            var parameterNames = signature.Split(',').ToArray();
            code = code.Substring(closeBracket + 1);
            code = code.Trim();
            code = code.Substring(1, code.Length - 2).Trim();
            var returnRemoved = false;
            if (code.StartsWith("return ")
                || code.StartsWith("return\t")
                || code.StartsWith("return("))
            {
                code = code.Substring(6).Trim();
                returnRemoved = true;
            }
            if (code.EndsWith(";"))
            {
                code = code.Substring(0, code.Length - 1).Trim();
            }
            return new JavaScriptFunctionBody(parameterNames, code, signature, original, $"function ({signature}) {{ {(returnRemoved ? "return " : "")}{code}; }}");
        }
    }
}