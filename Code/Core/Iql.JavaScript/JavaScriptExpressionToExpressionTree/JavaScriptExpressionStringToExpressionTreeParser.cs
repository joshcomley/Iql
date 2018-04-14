using System;
using System.Collections.Generic;
using System.Linq;
using Iql.JavaScript.Extensions;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Nodes;
using Iql.JavaScript.JavaScriptExpressionToExpressionTree.Operators;

namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    //     JavaScript Expression Parser (JSEP) <%= version %>
    //     JSEP may be freely distributed under the MIT License
    //     http://jsep.from.so/
    public class JavaScriptExpressionStringToExpressionTreeParser
    {
        private static Dictionary<string, JavaScriptExpressionNode> _parsed = new Dictionary<string, JavaScriptExpressionNode>();
        private readonly string _expr;
        private int _index;

        private readonly int _length;
        private readonly JavaScriptParserSettings _settings;

        public JavaScriptExpressionStringToExpressionTreeParser(string expr, 
            JavaScriptParserSettings settings = null,
            bool extractBody = true)
        {
            if (extractBody)
            {
                var body = JavaScriptCodeExtractor.ExtractBody(expr);
                _expr = body.Body;
            }
            else
            {
                _expr = expr;
            }
            _settings = settings ?? new JavaScriptParserSettings();
            _length = _expr.Length;
        }

        public char ExprI(int i)
        {
            return i >= _expr.Length ? (char) 0 : _expr[i];
        }

        public int ExprICode(int i)
        {
            return i >= _expr.Length ? 0 : _expr[i];
        }

        // Parsing
        // -------
        // `expr` is a string with the passed in expression
        public JavaScriptExpressionNode Parse()
        {
            if (_parsed.ContainsKey(_expr))
            {
                return _parsed[_expr];
            }
            // `index` stores the character number we are currently at while `length` is a constant
            // All of the reads below will modify `index` as we move along
            var nodes = new List<JavaScriptExpressionNode>();

            while (_index < _length)
            {
                var chI = ExprICode(_index);

                // Expressions can be separated by semicolons, commas, or just inferred without object
                // separators
                if (chI == JavaScriptParserSettings.Semicolon || chI == JavaScriptParserSettings.Comma)
                {
                    _index++; // ignore separators
                }
                else
                {
                    // Try to read each expression individually
                    JavaScriptExpressionNode node;
                    if ((node = ReadExpression()) != null)
                    {
                        nodes.Add(node);
                        // If we weren't able to find a binary expression and are out of room, then
                        // the expression passed in probably has too much
                    }
                    else if (_index < _length)
                    {
                        JavaScriptParserSettings.ThrowError(_expr, "Unexpected \"" + ExprI(_index) + "\"", _index);
                    }
                }
            }

            // If there's only one expression just try returning the expression
            var result = nodes.Count == 1 ? nodes[0] : new CompoundJavaScriptExpressionNode(nodes);
            _parsed.Add(_expr, result);
            return result;
        }

        // Push `this.index` up to the next non-space character
        public void ReadSpaces()
        {
            var ch = ExprICode(_index);
            // space or tab
            while (ch == 32 || ch == 9)
            {
                ch = ExprICode(++_index);
            }
        }

        // The main parsing function. Much of this code is dedicated to ternary expressions
        public JavaScriptExpressionNode ReadExpression()
        {
            var test = ReadBinaryExpression();
            ReadSpaces();
            if (ExprICode(_index) == JavaScriptParserSettings.QuestionMark)
            {
                // Ternary expression: test ? consequent : alternate
                _index++;
                var consequent = ReadExpression();
                if (consequent == null)
                {
                    JavaScriptParserSettings.ThrowError(_expr, "Expected expression", _index);
                }
                ReadSpaces();
                if (ExprICode(_index) == JavaScriptParserSettings.ColonCode)
                {
                    _index++;
                    var alternate = ReadExpression();
                    if (alternate == null)
                    {
                        JavaScriptParserSettings.ThrowError(_expr, "Expected expression", _index);
                    }
                    return new ConditionalJavaScriptExpressionNode(test, consequent, alternate);
                }
                JavaScriptParserSettings.ThrowError(_expr, "Expected :", _index);
            }
            else
            {
                return test;
            }
            return null;
        }

        // Search for the operation portion of the string (e.g. `+`, `==`)
        // Start by taking the longest possible binary operations (3 characters: `==`, `!==`, `>>>`)
        // and move down from 3 to 2 to 1 character until a matching binary operation is found
        // then, return that binary operation
        public string ReadBinaryOp()
        {
            ReadSpaces();
            var toCheck = _expr.SubstringSafe(_index, _settings.MaxBinopLen);
            var tcLen = toCheck.Length;
            while (tcLen > 0)
            {
                if (_settings.BinaryOps.ContainsKey(toCheck))
                {
                    _index += tcLen;
                    return toCheck;
                }
                toCheck = toCheck.Substring(0, --tcLen);
            }
            return null;
        }

        // This function is responsible for gobbling an individual expression,
        // e.g. `1`, `1+2`, `a+(b*2)-Math.sqrt(2)`
        public JavaScriptExpressionNode ReadBinaryExpression()
        {
            JavaScriptExpressionNode node;

            // First, try to get the leftmost thing
            // Then, check to see if there's a binary operator operating on that leftmost thing
            var left = ReadToken();
            var biop = ReadBinaryOp();

            // If there wasn't a binary _operator, just return the leftmost node
            if (biop == null)
            {
                return left;
            }

            if (biop == "=>")
            {
                var lambda = ReadLambda();
                var property = left as PropertyIdentifierJavaScriptExpressionNode;
                //lambda = $"function({property.Name}) {{ return {lambda}; }}";
                var parser = new JavaScriptExpressionStringToExpressionTreeParser(
                    lambda, 
                    _settings,
                    false);
                var result = parser.Parse();
                return new LambdaJavaScriptExpressionNode(property.Name, result);
            }

            // Otherwise, we need to start a stack to properly place the binary operations in their
            // precedence structure
            var biopInfo = new BinaryOperationInfo {Value = biop, Prec = _settings.BinaryPrecedence(biop)};

            var right = ReadToken();
            if (right == null)
            {
                JavaScriptParserSettings.ThrowError(_expr, "Expected expression after " + biop, _index);
            }
            var stack = new List<object>(new object[] {left, biopInfo, right});

            // Properly deal with precedence using [recursive descent](http://www.engr.mun.ca/~theo/Misc/exp_parsing.htm)
            while ((biop = ReadBinaryOp()) != null)
            {
                var prec = _settings.BinaryPrecedence(biop);

                if (prec == 0)
                {
                    break;
                }
                biopInfo = new BinaryOperationInfo {Value = biop, Prec = prec};

                // Reduce: make a binary expression from the three topmost entries.
                while (stack.Count > 2 && prec <= (stack[stack.Count - 2] as BinaryOperationInfo).Prec)
                {
                    right = stack.PopAs<JavaScriptExpressionNode>();
                    biop = stack.PopAs<BinaryOperationInfo>().Value;
                    left = stack.PopAs<JavaScriptExpressionNode>();
                    node = new BinaryJavaScriptExpressionNode(
                        OperatorMap.OperatorTypes.ResolveValue(biop), left, right);
                    stack.Add(node);
                }

                node = ReadToken();
                if (node == null)
                {
                    JavaScriptParserSettings.ThrowError(_expr, "Expected expression after " + biop, _index);
                }
                stack.Add(biopInfo);
                stack.Add(node);
            }

            var i = stack.Count - 1;
            node = stack[i] as JavaScriptExpressionNode;
            while (i > 1)
            {
                node = new BinaryJavaScriptExpressionNode(
                    OperatorMap.OperatorTypes.ResolveValue((stack[i - 1] as BinaryOperationInfo).Value),
                    stack[i - 2] as JavaScriptExpressionNode,
                    node);
                i -= 2;
            }
            return node;
        }

        private string ReadLambda()
        {
            ReadSpaces();
            var depth = 0;
            var lambdaEnd = new[]
            {
                JavaScriptParserSettings.CloseArray,
                JavaScriptParserSettings.CloseParenthesis,
                JavaScriptParserSettings.CloseScope,
                JavaScriptParserSettings.Comma,
            };
            var depthIncrement = new[]
            {
                JavaScriptParserSettings.OpenArray,
                JavaScriptParserSettings.OpenParenthesis,
                JavaScriptParserSettings.OpenScope,
            };
            var depthDecrement = new[]
            {
                JavaScriptParserSettings.CloseArray,
                JavaScriptParserSettings.CloseParenthesis,
                JavaScriptParserSettings.CloseScope,
            };
            var str = "";
            while (true)
            {
                if (_index >= _expr.Length)
                {
                    break;
                }
                var ch = _expr[_index];
                if (depth == 0 && lambdaEnd.Contains(ch))
                {
                    break;
                }
                if (depthIncrement.Contains(ch))
                {
                    depth++;
                }
                else if (depthDecrement.Contains(ch))
                {
                    depth--;
                }

                str += ch;
                _index++;
            }
            return str;
        }

        // An individual part of a binary expression:
        // e.g. `foo.bar(baz)`, `1`, `"abc"`, `(a % 2)` (because it's in parenthesis)
        public JavaScriptExpressionNode ReadToken()
        {
            int ch;
            int tcLen;

            ReadSpaces();
            ch = ExprICode(_index);

            if (_settings.IsDecimalDigit(ch) || ch == JavaScriptParserSettings.Period)
            {
                // Char code 46 is a dot `.` which can start off a numeric literal
                return ReadNumericLiteral();
            }
            if (ch == JavaScriptParserSettings.SingleQuote || ch == JavaScriptParserSettings.DoubleQuote || ch == JavaScriptParserSettings.DashQuote)
            {
                // Single or double quotes
                return ReadStringLiteral();
            }
            if (_settings.IsIdentifierStart(ch) || ch == JavaScriptParserSettings.OpenParenthesis)
            {
                // open parenthesis
                // `foo`, `bar.baz`
                return ReadVariable();
            }
            if (ch == JavaScriptParserSettings.OpenArray)
            {
                return ReadArray();
            }
            var toCheck = _expr.Substring(_index, _settings.MaxUnopLen);
            tcLen = toCheck.Length;
            while (tcLen > 0)
            {
                if (_settings.UnaryOps.ContainsKey(toCheck))
                {
                    _index += tcLen;
                    return new UnaryJavaScriptExpressionNode(OperatorMap.OperatorTypes.ResolveValue(toCheck),
                        ReadToken(), true);
                }
                toCheck = toCheck.Substring(0, --tcLen);
            }

            return null;
        }

        // Parse simple numeric literals: `12`, `3.4`, `.5`. Do this by using a string to
        // keep track of everything in the numeric literal and then calling `parseFloat` on that string
        public JavaScriptExpressionNode ReadNumericLiteral()
        {
            var number = "";
            int ch;
            int chCode;
            while (_settings.IsDecimalDigit(ExprICode(_index)))
            {
                number += ExprI(_index++);
            }

            if (ExprICode(_index) == JavaScriptParserSettings.Period)
            {
                // can start with a decimal marker
                number += ExprI(_index++);

                while (_settings.IsDecimalDigit(ExprICode(_index)))
                {
                    number += ExprI(_index++);
                }
            }

            ch = ExprI(_index);
            if (ch == 'e' || ch == 'E')
            {
                // exponent marker
                number += ExprI(_index++);
                ch = ExprI(_index);
                if (ch == '+' || ch == '-')
                {
                    // exponent sign
                    number += ExprI(_index++);
                }
                while (_settings.IsDecimalDigit(ExprICode(_index)))
                {
                    //exponent itself
                    number += ExprI(_index++);
                }
                if (!_settings.IsDecimalDigit(ExprICode(_index - 1)))
                {
                    JavaScriptParserSettings.ThrowError(_expr, "Expected exponent (" + number + ExprI(_index) + ')', _index);
                }
            }


            chCode = ExprICode(_index);
            // Check to make sure this isn't a variable name that start with a number (123abc)
            if (_settings.IsIdentifierStart(chCode))
            {
                JavaScriptParserSettings.ThrowError(_expr, "Variable names cannot start with a number (" +
                                                    number + ExprI(_index) + ')', _index);
            }
            else if (chCode == JavaScriptParserSettings.Period)
            {
                JavaScriptParserSettings.ThrowError(_expr, "Unexpected period", _index);
            }

            return new LiteralJavaScriptExpressionNode(double.Parse(number), number);
        }

        // Parses a string literal, staring with single or double quotes with basic support for escape codes
        // e.g. `"hello world"`, `'this is\nJSEP'`
        public JavaScriptExpressionNode ReadStringLiteral()
        {
            var str = "";
            var quote = ExprI(_index++);
            var closed = false;

            while (_index < _length)
            {
                var ch = ExprI(_index++);
                if (ch == quote)
                {
                    closed = true;
                    break;
                }
                if (ch == '\\')
                {
                    // Check for all of the common escape codes
                    ch = ExprI(_index++);
                    switch (ch)
                    {
                        case 'n':
                            str += '\n';
                            break;
                        case 'r':
                            str += '\r';
                            break;
                        case 't':
                            str += '\t';
                            break;
                        case 'b':
                            str += '\b';
                            break;
                        case 'f':
                            str += '\f';
                            break;
                        case 'v':
                            str += '\x0B';
                            break;
                        default:
                            str += '\\' + ch;
                            break;
                    }
                }
                else
                {
                    str += ch;
                }
            }

            if (!closed)
            {
                JavaScriptParserSettings.ThrowError(_expr, "Unclosed quote after \"" + str + "\"", _index);
            }
            var node = new LiteralJavaScriptExpressionNode(str, quote + str + quote);
            ReadSpaces();
            return ReadSubCalls(node);
        }

        // Reads only identifiers
        // e.g.: `foo`, `_value`, `$x1`
        // Also, this function checks if that identifier is a literal:
        // (e.g. `true`, `false`, `null`) or `this`
        public JavaScriptExpressionNode ReadIdentifier()
        {
            var ch = ExprICode(_index);
            var start = _index;

            if (_settings.IsIdentifierStart(ch))
            {
                _index++;
            }
            else
            {
                JavaScriptParserSettings.ThrowError(_expr, "Unexpected " + ExprI(_index), _index);
            }

            while (_index < _length)
            {
                ch = ExprICode(_index);
                if (_settings.IsIdentifierPart(ch))
                {
                    _index++;
                }
                else
                {
                    break;
                }
            }
            var identifier = _expr.Substring(start, _index - start);

            if (_settings.Literals.ContainsKey(identifier))
            {
                return new LiteralJavaScriptExpressionNode(_settings.Literals[identifier], identifier);
            }
            if (identifier == JavaScriptParserSettings.ThisStr)
            {
                return new ThisJavaScriptExpressionNode();
            }
            return new PropertyIdentifierJavaScriptExpressionNode(identifier);
        }

        // Reads a list of arguments within the context of a function call
        // or array literal. This function also assumes that the opening character
        // `(` or `[` has already been readd, and reads expressions and commas
        // until the terminator character `)` or `]` is encountered.
        // e.g. `foo(bar, baz)`, `my_func()`, or `[bar, baz]`
        public List<JavaScriptExpressionNode> ReadArguments(int termination)
        {
            var args = new List<JavaScriptExpressionNode>();
            var closed = false;
            while (_index < _length)
            {
                ReadSpaces();
                var chI = ExprICode(_index);
                if (chI == termination)
                {
                    // done parsing
                    closed = true;
                    _index++;
                    break;
                }
                if (chI == JavaScriptParserSettings.Comma)
                {
                    // between expressions
                    _index++;
                }
                else
                {
                    var node = ReadExpression();
                    if (node == null || node.Type == ExpressionType.Compound)
                    {
                        JavaScriptParserSettings.ThrowError(_expr, "Expected comma", _index);
                    }
                    args.Add(node);
                }
            }
            if (!closed)
            {
                JavaScriptParserSettings.ThrowError(_expr, "Expected " + termination, _index);
            }
            return args;
        }

        // Read a non-literal variable name. This variable name may include properties
        // e.g. `foo`, `bar.baz`, `foo['bar'].baz`
        // It also reads function calls:
        // e.g. `Math.acos(obj.angle)`
        public JavaScriptExpressionNode ReadVariable()
        {
            int chI;
            chI = ExprICode(_index);

            var node = chI == JavaScriptParserSettings.OpenParenthesis ? ReadGroup() : ReadIdentifier();
            ReadSpaces();
            return ReadSubCalls(node);
        }

        public JavaScriptExpressionNode ReadSubCalls(JavaScriptExpressionNode node)
        {
            var chI = ExprICode(_index);
            while (chI == JavaScriptParserSettings.Period || chI == JavaScriptParserSettings.OpenArray ||
                   chI == JavaScriptParserSettings.OpenParenthesis)
            {
                _index++;
                switch (chI)
                {
                    case JavaScriptParserSettings.Period:
                        ReadSpaces();
                        node = new MemberJavaScriptExpressionNode(
                            false,
                            node,
                            ReadIdentifier()
                        );
                        break;
                    case JavaScriptParserSettings.OpenArray:
                        node = new MemberJavaScriptExpressionNode(
                            true,
                            node,
                            ReadExpression()
                        );
                        ReadSpaces();
                        chI = ExprICode(_index);
                        if (chI != JavaScriptParserSettings.CloseArray)
                        {
                            JavaScriptParserSettings.ThrowError(_expr, "Unclosed [", _index);
                        }
                        _index++;
                        break;
                    case JavaScriptParserSettings.OpenParenthesis:
                        // A function call is being made; read all the arguments
                        node = new CallJavaScriptExpressionNode(
                            ReadArguments(JavaScriptParserSettings.CloseParenthesis),
                            node);
                        break;
                }
                ReadSpaces();
                chI = ExprICode(_index);
            }
            return node;
        }

        // Responsible for parsing a group of things within parentheses `()`
        // This function assumes that it needs to read the opening parenthesis
        // and then tries to read everything within that parenthesis, assuming
        // that the next thing it should see is the close parenthesis. If not,
        // then the expression probably doesn't have a `)`
        public JavaScriptExpressionNode ReadGroup()
        {
            _index++;
            var node = ReadExpression();
            ReadSpaces();
            if (ExprICode(_index) == JavaScriptParserSettings.CloseParenthesis)
            {
                _index++;
                return node;
            }
            JavaScriptParserSettings.ThrowError(_expr, "Unclosed (", _index);
            return null;
        }

        // Responsible for parsing Array literals `[1, 2, 3]`
        // This function assumes that it needs to read the opening bracket
        // and then tries to read the expressions as arguments.
        public JavaScriptExpressionNode ReadArray()
        {
            _index++;
            return new ArrayJavaScriptExpressionNode(ReadArguments(JavaScriptParserSettings.CloseArray));
        }

        private class BinaryOperationInfo
        {
            public string Value { get; set; }
            public int Prec { get; set; }
        }
    }
}