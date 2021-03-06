﻿namespace Iql.JavaScript
{
    public class JavaScriptExpression
    {
        public JavaScriptExpression(
            string rootVariableName,
            string expression)
        {
            RootVariableName = rootVariableName;
            Expression = expression;
        }

        public string RootVariableName { get; }
        public string Expression { get; }

        public string AsFunction(bool typeScript = false)
        {
            if (Expression.StartsWith("function("))
            {
                if (typeScript)
                {
                    var body = JavaScriptCodeExtractor.ExtractBody(Expression);
                    var parameters = "";
                    if (body.ParameterNames != null)
                    {
                        parameters = string.Join(", ", body.ParameterNames);
                        if (body.ParameterNames.Length > 1)
                        {
                            parameters = $"({parameters})";
                        }
                    }

                    return $"{parameters} => {body.Body}";
                }
                return Expression;
            }
            if (typeScript)
            {
                return $"{RootVariableName} => {Expression}";
            }
            return $"function({RootVariableName}) {{ return {Expression}; }}";
        }
    }
}