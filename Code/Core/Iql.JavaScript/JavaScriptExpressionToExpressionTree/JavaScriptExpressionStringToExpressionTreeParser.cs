using System.Collections.Generic;

namespace Iql.JavaScript.JavaScriptExpressionToExpressionTree
{
    //     JavaScript Expression Parser (JSEP) <%= version %>
    //     JSEP may be freely distributed under the MIT License
    //     http://jsep.from.so/

    /*global module: true, publics: true, console: true */

    public class JavaScriptExpressionStringToExpressionTreeParser
    {
        private readonly string _expr;
        private int _index;

        private readonly int _length;
        private readonly JavaScriptParserSettings _settings;

        public JavaScriptExpressionStringToExpressionTreeParser(string expr, JavaScriptParserSettings settings = null)
        {
            var body = JavaScriptCodeExtractor.ExtractBody(expr);
            _settings = settings ?? new JavaScriptParserSettings();
            _expr = body.Body;
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
            // `index` stores the character number we are currently at while `length` is a constant
            // All of the gobbles below will modify `index` as we move along
            var nodes = new List<JavaScriptExpressionNode>();
            int chI;
            JavaScriptExpressionNode node;

            while (_index < _length)
            {
                chI = ExprICode(_index);

                // Expressions can be separated by semicolons, commas, or just inferred without object
                // separators
                if (chI == JavaScriptParserSettings.SemcolCode || chI == JavaScriptParserSettings.CommaCode)
                {
                    _index++; // ignore separators
                }
                else
                {
                    // Try to gobble each expression individually
                    if ((node = GobbleExpression()) != null)
                    {
                        nodes.Add(node);
                        // If we weren't able to find a binary expression and are out of room, then
                        // the expression passed in probably has too much
                    }
                    else if (_index < _length)
                    {
                        JavaScriptParserSettings.ThrowError("Unexpected \"" + ExprI(_index) + "\"", _index);
                    }
                }
            }

            // If there's only one expression just try returning the expression
            if (nodes.Count == 1)
            {
                return nodes[0];
            }
            return new CompoundJavaScriptExpressionNode(nodes);
        }

        // Push `this.index` up to the next non-space character
        public void GobbleSpaces()
        {
            var ch = ExprICode(_index);
            // space or tab
            while (ch == 32 || ch == 9)
            {
                ch = ExprICode(++_index);
            }
        }

        // The main parsing function. Much of this code is dedicated to ternary expressions
        public JavaScriptExpressionNode GobbleExpression()
        {
            var test = GobbleBinaryExpression();
            JavaScriptExpressionNode consequent;
            JavaScriptExpressionNode alternate;
            GobbleSpaces();
            if (ExprICode(_index) == JavaScriptParserSettings.QumarkCode)
            {
                // Ternary expression: test ? consequent : alternate
                _index++;
                consequent = GobbleExpression();
                if (consequent == null)
                {
                    JavaScriptParserSettings.ThrowError("Expected expression", _index);
                }
                GobbleSpaces();
                if (ExprICode(_index) == JavaScriptParserSettings.ColonCode)
                {
                    _index++;
                    alternate = GobbleExpression();
                    if (alternate == null)
                    {
                        JavaScriptParserSettings.ThrowError("Expected expression", _index);
                    }
                    return new ConditionalJavaScriptExpressionNode(test, consequent, alternate);
                }
                JavaScriptParserSettings.ThrowError("Expected :", _index);
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
        public string GobbleBinaryOp()
        {
            GobbleSpaces();
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
        public JavaScriptExpressionNode GobbleBinaryExpression()
        {
            int chI, i, prec;
            JavaScriptExpressionNode node, left, right;
            BinaryOperationInfo biopInfo;
            string biop;
            List<object> stack;

            // First, try to get the leftmost thing
            // Then, check to see if there's a binary operator operating on that leftmost thing
            left = GobbleToken();
            biop = GobbleBinaryOp();

            // If there wasn't a binary _operator, just return the leftmost node
            if (biop == null)
            {
                return left;
            }

            // Otherwise, we need to start a stack to properly place the binary operations in their
            // precedence structure
            biopInfo = new BinaryOperationInfo {Value = biop, Prec = _settings.BinaryPrecedence(biop)};

            right = GobbleToken();
            if (right == null)
            {
                JavaScriptParserSettings.ThrowError("Expected expression after " + biop, _index);
            }
            stack = new List<object>(new object[] {left, biopInfo, right});

            // Properly deal with precedence using [recursive descent](http://www.engr.mun.ca/~theo/Misc/exp_parsing.htm)
            while ((biop = GobbleBinaryOp()) != null)
            {
                prec = _settings.BinaryPrecedence(biop);

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

                node = GobbleToken();
                if (node == null)
                {
                    JavaScriptParserSettings.ThrowError("Expected expression after " + biop, _index);
                }
                stack.Add(biopInfo);
                stack.Add(node);
            }

            i = stack.Count - 1;
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

        // An individual part of a binary expression:
        // e.g. `foo.bar(baz)`, `1`, `"abc"`, `(a % 2)` (because it's in parenthesis)
        public JavaScriptExpressionNode GobbleToken()
        {
            int ch;
            string toCheck;
            int tcLen;

            GobbleSpaces();
            ch = ExprICode(_index);

            if (_settings.IsDecimalDigit(ch) || ch == JavaScriptParserSettings.PeriodCode)
            {
                // Char code 46 is a dot `.` which can start off a numeric literal
                return GobbleNumericLiteral();
            }
            if (ch == JavaScriptParserSettings.SquoteCode || ch == JavaScriptParserSettings.DquoteCode)
            {
                // Single or double quotes
                return GobbleStringLiteral();
            }
            if (_settings.IsIdentifierStart(ch) || ch == JavaScriptParserSettings.OparenCode)
            {
                // open parenthesis
                // `foo`, `bar.baz`
                return GobbleVariable();
            }
            if (ch == JavaScriptParserSettings.ObrackCode)
            {
                return GobbleArray();
            }
            toCheck = _expr.Substring(_index, _settings.MaxUnopLen);
            tcLen = toCheck.Length;
            while (tcLen > 0)
            {
                if (_settings.UnaryOps.ContainsKey(toCheck))
                {
                    _index += tcLen;
                    return new UnaryJavaScriptExpressionNode(OperatorMap.OperatorTypes.ResolveValue(toCheck),
                        GobbleToken(), true);
                }
                toCheck = toCheck.Substring(0, --tcLen);
            }

            return null;
        }

        // Parse simple numeric literals: `12`, `3.4`, `.5`. Do this by using a string to
        // keep track of everything in the numeric literal and then calling `parseFloat` on that string
        public JavaScriptExpressionNode GobbleNumericLiteral()
        {
            var number = "";
            int ch;
            int chCode;
            while (_settings.IsDecimalDigit(ExprICode(_index)))
            {
                number += ExprI(_index++);
            }

            if (ExprICode(_index) == JavaScriptParserSettings.PeriodCode)
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
                    JavaScriptParserSettings.ThrowError("Expected exponent (" + number + ExprI(_index) + ')', _index);
                }
            }


            chCode = ExprICode(_index);
            // Check to make sure this isn't a variable name that start with a number (123abc)
            if (_settings.IsIdentifierStart(chCode))
            {
                JavaScriptParserSettings.ThrowError("Variable names cannot start with a number (" +
                                                    number + ExprI(_index) + ')', _index);
            }
            else if (chCode == JavaScriptParserSettings.PeriodCode)
            {
                JavaScriptParserSettings.ThrowError("Unexpected period", _index);
            }

            return new LiteralJavaScriptExpressionNode(double.Parse(number), number);
        }

        // Parses a string literal, staring with single or double quotes with basic support for escape codes
        // e.g. `"hello world"`, `'this is\nJSEP'`
        public JavaScriptExpressionNode GobbleStringLiteral()
        {
            var str = "";
            var quote = ExprI(_index++);
            var closed = false;
            char ch;

            while (_index < _length)
            {
                ch = ExprI(_index++);
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
                JavaScriptParserSettings.ThrowError("Unclosed quote after \"" + str + "\"", _index);
            }
            var node = new LiteralJavaScriptExpressionNode(str, quote + str + quote);
            GobbleSpaces();
            return GobbleSubCalls(node);
        }

        // Gobbles only identifiers
        // e.g.: `foo`, `_value`, `$x1`
        // Also, this function checks if that identifier is a literal:
        // (e.g. `true`, `false`, `null`) or `this`
        public JavaScriptExpressionNode GobbleIdentifier()
        {
            var ch = ExprICode(_index);
            var start = _index;
            string identifier;

            if (_settings.IsIdentifierStart(ch))
            {
                _index++;
            }
            else
            {
                JavaScriptParserSettings.ThrowError("Unexpected " + ExprI(_index), _index);
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
            identifier = _expr.Substring(start, _index - start);

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

        // Gobbles a list of arguments within the context of a function call
        // or array literal. This function also assumes that the opening character
        // `(` or `[` has already been gobbled, and gobbles expressions and commas
        // until the terminator character `)` or `]` is encountered.
        // e.g. `foo(bar, baz)`, `my_func()`, or `[bar, baz]`
        public List<JavaScriptExpressionNode> GobbleArguments(int termination)
        {
            var args = new List<JavaScriptExpressionNode>();
            int chI;
            JavaScriptExpressionNode node;
            var closed = false;
            while (_index < _length)
            {
                GobbleSpaces();
                chI = ExprICode(_index);
                if (chI == termination)
                {
                    // done parsing
                    closed = true;
                    _index++;
                    break;
                }
                if (chI == JavaScriptParserSettings.CommaCode)
                {
                    // between expressions
                    _index++;
                }
                else
                {
                    node = GobbleExpression();
                    if (node == null || node.Type == ExpressionType.Compound)
                    {
                        JavaScriptParserSettings.ThrowError("Expected comma", _index);
                    }
                    args.Add(node);
                }
            }
            if (!closed)
            {
                JavaScriptParserSettings.ThrowError("Expected " + termination, _index);
            }
            return args;
        }

        // Gobble a non-literal variable name. This variable name may include properties
        // e.g. `foo`, `bar.baz`, `foo['bar'].baz`
        // It also gobbles function calls:
        // e.g. `Math.acos(obj.angle)`
        public JavaScriptExpressionNode GobbleVariable()
        {
            int chI;
            JavaScriptExpressionNode node;
            chI = ExprICode(_index);

            if (chI == JavaScriptParserSettings.OparenCode)
            {
                node = GobbleGroup();
            }
            else
            {
                node = GobbleIdentifier();
            }
            GobbleSpaces();
            return GobbleSubCalls(node);
        }

        public JavaScriptExpressionNode GobbleSubCalls(JavaScriptExpressionNode node)
        {
            var chI = ExprICode(_index);
            while (chI == JavaScriptParserSettings.PeriodCode || chI == JavaScriptParserSettings.ObrackCode ||
                   chI == JavaScriptParserSettings.OparenCode)
            {
                _index++;
                if (chI == JavaScriptParserSettings.PeriodCode)
                {
                    GobbleSpaces();
                    node = new MemberJavaScriptExpressionNode(
                        false,
                        node,
                        GobbleIdentifier()
                    );
                }
                else if (chI == JavaScriptParserSettings.ObrackCode)
                {
                    node = new MemberJavaScriptExpressionNode(
                        true,
                        node,
                        GobbleExpression()
                    );
                    GobbleSpaces();
                    chI = ExprICode(_index);
                    if (chI != JavaScriptParserSettings.CbrackCode)
                    {
                        JavaScriptParserSettings.ThrowError("Unclosed [", _index);
                    }
                    _index++;
                }
                else if (chI == JavaScriptParserSettings.OparenCode)
                {
                    // A function call is being made; gobble all the arguments
                    node = new CallJavaScriptExpressionNode(
                        GobbleArguments(JavaScriptParserSettings.CparenCode),
                        node);
                }
                GobbleSpaces();
                chI = ExprICode(_index);
            }
            return node;
        }

        // Responsible for parsing a group of things within parentheses `()`
        // This function assumes that it needs to gobble the opening parenthesis
        // and then tries to gobble everything within that parenthesis, assuming
        // that the next thing it should see is the close parenthesis. If not,
        // then the expression probably doesn't have a `)`
        public JavaScriptExpressionNode GobbleGroup()
        {
            _index++;
            var node = GobbleExpression();
            GobbleSpaces();
            if (ExprICode(_index) == JavaScriptParserSettings.CparenCode)
            {
                _index++;
                return node;
            }
            JavaScriptParserSettings.ThrowError("Unclosed (", _index);
            return null;
        }

        // Responsible for parsing Array literals `[1, 2, 3]`
        // This function assumes that it needs to gobble the opening bracket
        // and then tries to gobble the expressions as arguments.
        public JavaScriptExpressionNode GobbleArray()
        {
            _index++;
            return new ArrayJavaScriptExpressionNode(GobbleArguments(JavaScriptParserSettings.CbrackCode));
        }

        private class BinaryOperationInfo
        {
            public string Value { get; set; }
            public int Prec { get; set; }
        }
    }
}