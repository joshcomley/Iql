using System;
using System.Collections.Generic;

namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    public class JavaScriptParserSettings
    {
        // This is the full set of types that object JSEP node can be.
        // Store them here to save space when minified

        public const int Period = 46; // '.'
        public const int Comma = 44; // ','
        public const int SingleQuote = 39; // single quote
        public const int DashQuote = 96; // ` quote
        public const int DoubleQuote = 34; // double quotes
        public const int OpenParenthesis = 40; // (
        public const int CloseParenthesis = 41; // )
        public const int OpenScope = 123; // {
        public const int CloseScope = 125; // }
        public const int OpenArray = 91; // [
        public const int CloseArray = 93; // ]
        public const int QuestionMark = 63; // ?
        public const int Semicolon = 59; // ;

        public const int ColonCode = 58; // :

        // Except for `this`, which is special. This could be changed to something like `'self'` as well
        public static string ThisStr = "this";

        // Operations
        // ----------

        // Set `t` to `true` to save space (when minified, not gzipped)
        private readonly bool _t = true;

        public Dictionary<string, int> BinaryOps;

        public Dictionary<string, object> Literals;

        public int MaxBinopLen;
        public int MaxUnopLen;

        public Dictionary<string, bool> UnaryOps;

        // To be filled in by the template
        public string Version = "<%= version %>";

        public JavaScriptParserSettings()
        {
            _t = true;
            // Use a quickly-accessible map to store all of the unary operators
            // Values are set to `true` (it really doesn't matter)
            UnaryOps = new Dictionary<string, bool> {{"-", _t}, {"!", _t}, {"~", _t}, {"+", _t}};
            // Also use a map for the binary operations but set their values to their
            // binary precedence for quick reference:
            // see [Order of operations](http://en.wikipedia.org/wiki/Order_of_operations#Programming_language)
            BinaryOps = new Dictionary<string, int>
            {
                {"||", 1},
                {"&&", 2},
                {"|", 3},
                {"^", 4},
                {"&", 5},
                {"==", 6},
                {"!=", 6},
                {"===", 6},
                {"!===", 6},
                {"<", 7},
                {">", 7},
                {"<=", 7},
                {">=", 7},
                {"<<", 8},
                {">>", 8},
                {">>>", 8},
                {"+", 9},
                {"-", 9},
                {"*", 10},
                {"/", 10},
                {"%", 10},
                {"=>", 11}
            };
            MaxUnopLen = GetMaxKeyLen(UnaryOps);
            MaxBinopLen = GetMaxKeyLen(BinaryOps);
            // Literals
            // ----------
            // Store the values to return for the various literals we may encounter
            Literals = new Dictionary<string, object>
            {
                {"true", true},
                {"false", false},
                {"null", null}
            };
        }

        // Get return the longest key length of object object
        public static int GetMaxKeyLen<TValue>(IDictionary<string, TValue> obj)
        {
            var maxLen = 0;
            var len = 0;
            foreach (var key in obj.Keys)
            {
                if ((len = key.Length) > maxLen)
                {
                    maxLen = len;
                }
            }
            return maxLen;
        }

        // Returns the precedence of a binary _operator or `0` if it isn't a binary _operator
        public int BinaryPrecedence(string opVal)
        {
            if (BinaryOps.ContainsKey(opVal))
            {
                return BinaryOps[opVal];
            }
            return 0;
        }

        // `ch` is a character code in the next three functions
        public bool IsDecimalDigit(int ch)
        {
            return ch >= 48 && ch <= 57; // 0...9
        }

        public bool IsIdentifierStart(int ch)
        {
            return ch == 36 || ch == 95 || // `$` and `_`
                   ch >= 65 && ch <= 90 || // A...Z
                   ch >= 97 && ch <= 122 || // a...z
                   ch >= 128 && !BinaryOps.ContainsKey(ch + ""); // object non-ASCII that is not an _operator
        }

        public bool IsIdentifierPart(int ch)
        {
            return ch == 36 || ch == 95 || // `$` and `_`
                   ch >= 65 && ch <= 90 || // A...Z
                   ch >= 97 && ch <= 122 || // a...z
                   ch >= 48 && ch <= 57 || // 0...9
                   ch >= 128 && !BinaryOps.ContainsKey(ch + ""); // object non-ASCII that is not an _operator
        }

        public string toString()
        {
            return "JavaScript Expression Parser (JSEP) v" + Version;
        }

        public static void ThrowError(string source, string message, int index)
        {
            var error = new Exception(message + " at character " + index + ":\r\n\r\n" + source);
            //error["index"] = index;
            //error["description"] = message;
            throw error;
        }

        /**
         * @method public addUnaryOp
         * @param {string} op_name The name of the unary op to add
         * @return jsep
         */
        public JavaScriptParserSettings AddUnaryOp(string opName)
        {
            MaxUnopLen = Math.Max(opName.Length, MaxUnopLen);
            UnaryOps[opName] = _t;
            return this;
        }

        /**
         * @method public addBinaryOp
         * @param {string} op_name The name of the binary op to add
         * @param {number} precedence The precedence of the binary op (can be a float)
         * @return jsep
         */
        public JavaScriptParserSettings AddBinaryOp(string opName, int precedence)
        {
            MaxBinopLen = Math.Max(opName.Length, MaxBinopLen);
            BinaryOps.Add(opName, precedence);
            return this;
        }

        /**
         * @method public addLiteral
         * @param {string} literal_name The name of the literal to add
         * @param {*} literal_value The value of the literal
         * @return jsep
         */
        public JavaScriptParserSettings AddLiteral(string literalName, object literalValue)
        {
            Literals.Add(literalName, literalValue);
            return this;
        }

        /**
         * @method public removeUnaryOp
         * @param {string} op_name The name of the unary op to remove
         * @return jsep
         */
        public JavaScriptParserSettings RemoveUnaryOp(string opName)
        {
            UnaryOps.Remove(opName);
            if (opName.Length == MaxUnopLen)
            {
                MaxUnopLen = GetMaxKeyLen(UnaryOps);
            }
            return this;
        }

        /**
         * @method public removeAllUnaryOps
         * @return jsep
         */
        public JavaScriptParserSettings RemoveAllUnaryOps()
        {
            UnaryOps.Clear();
            MaxUnopLen = 0;
            return this;
        }

        /**
         * @method public removeBinaryOp
         * @param {string} op_name The name of the binary op to remove
         * @return jsep
         */
        public JavaScriptParserSettings RemoveBinaryOp(string opName)
        {
            BinaryOps.Remove(opName);
            if (opName.Length == MaxBinopLen)
            {
                MaxBinopLen = GetMaxKeyLen(BinaryOps);
            }
            return this;
        }

        /**
         * @method public removeAllBinaryOps
         * @return jsep
         */
        public JavaScriptParserSettings RemoveAllBinaryOps()
        {
            BinaryOps.Clear();
            MaxBinopLen = 0;

            return this;
        }

        /**
         * @method public removeLiteral
         * @param {string} literal_name The name of the literal to remove
         * @return jsep
         */
        public JavaScriptParserSettings RemoveLiteral(string literalName)
        {
            Literals.Remove(literalName);
            return this;
        }

        /**
         * @method public removeAllLiterals
         * @return jsep
         */
        public JavaScriptParserSettings RemoveAllLiterals()
        {
            Literals.Clear();

            return this;
        }
    }
}