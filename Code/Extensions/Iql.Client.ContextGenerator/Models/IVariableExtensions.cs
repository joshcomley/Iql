using System;
using Iql.OData.TypeScript.Generator.Definitions;
using Iql.OData.TypeScript.Generator.Parsers;

namespace Iql.OData.TypeScript.Generator.Models
{
    public static class VariableExtensions
    {
        public static string AsTypeScriptParameter(this IVariable variable, ODataSchema schema, GeneratorSettings settings)
        {
            var parameter = variable as EntityFunctionParameterDefinition;
            return string.Format("{2}{0}{3}: {1}{4}",
                variable.Name,
                new TypeScriptTypeResolver(schema, settings).ResolveTypeNameFromODataName(variable.TypeInfo).Name,
                variable.Private ? "private " : "",
                parameter != null && parameter.TypeInfo.Nullable && !parameter.HasDefaultValue ? "?" : "",
                parameter != null && parameter.HasDefaultValue ? $" = {parameter.DefaultValue ?? "null"}" : ""
            );
        }

        public static string AsCSharpParameter(this IVariable variable, ODataSchema schema, GeneratorSettings settings)
        {
            var parameter = variable as EntityFunctionParameterDefinition;
            return string.Format("{0}{2} {1}{3}",
                new CSharpTypeResolver(schema, settings).ResolveTypeNameFromODataName(variable.TypeInfo).Name,
                variable.Name,
                parameter != null && parameter.TypeInfo.Nullable ? "?" : "",
            parameter != null && parameter.HasDefaultValue ? $" = {parameter.DefaultValue ?? "null"}" : ""
            );
        }
    }
}
