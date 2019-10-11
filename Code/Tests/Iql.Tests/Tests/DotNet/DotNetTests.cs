#if !TypeScript
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Iql.Conversion;
using Iql.Data;
using Iql.Data.Evaluation;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Entities.InferredValues;
using IqlSampleApp.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests.DotNet
{
    [TestClass]
    public class DotNetTests : TestsBase
    {
        [TestMethod]
        public void EnsureNullableParametersOnIqlExpressions()
        {
            var iqlExpressionTypes = typeof(IqlExpression).Assembly.GetTypes()
                .Where(t => typeof(IqlExpression).IsAssignableFrom(t) && !t.IsAbstract).ToArray();
            var success = true;
            var sb = new StringBuilder();
            foreach (var iqlExpressionType in iqlExpressionTypes)
            {
                var constructor = iqlExpressionType.GetConstructors()[0];
                foreach (var parameter in constructor.GetParameters())
                {
                    if (!parameter.HasDefaultValue && !parameter.IsOptional && parameter.GetCustomAttributes(typeof(ParamArrayAttribute), false).Length == 0)
                    {
                        sb.AppendLine(
                            $"At least one parameter (\"{parameter.Name}\") on constructor of type \"{iqlExpressionType.Name}\" does not have a default value.");
                        success = false;
                        break;
                    }
                }
            }
            Assert.IsTrue(success, sb.ToString());
        }

        [TestMethod]
        public void TestEnumComparisonParsers()
        {
            var iql = IqlConverter.Instance.ConvertLambdaToIql<PersonReport>(_ =>
                _.Status == FaultReportStatus.PassWithObservations,
                TypeResolver);
            var csharp = IqlConverter.Instance.ConvertIqlToExpressionString(iql.Expression,
                TypeResolver);
            Assert.AreEqual($"entity => ((int)entity.Status == 1)", csharp);
        }

        [TestMethod]
        public void GenerateIqlExpressionParsers()
        {
            var types = typeof(IqlExpression).Assembly.ExportedTypes
                .Where(t => typeof(IqlExpression).IsAssignableFrom(t))
                .Where(t => t.GetTypeInfo().DeclaredProperties.Any(p => typeof(IqlExpression).IsAssignableFrom(p.PropertyType) || typeof(IEnumerable<IqlExpression>).IsAssignableFrom(p.PropertyType)))
                .ToList();

            var declarationsSb = new StringBuilder();
            var registrationsSb = new StringBuilder();
            foreach (var type in types)
            {
                var properties = type.GetRuntimeProperties().Where(
                    p => typeof(IqlExpression).IsAssignableFrom(p.PropertyType) ||
                         typeof(IEnumerable<IqlExpression>).IsAssignableFrom(p.PropertyType));
                var propertySb = new StringBuilder();
                foreach (var property in properties)
                {
                    if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
                    {
                        propertySb.AppendLine(
                            $@"if(action.{property.Name} != null)
{{
    for(var i = 0; i < action.{property.Name}.Count; i++)
    {{
        action.{property.Name}[i] = ({property.PropertyType.GenericTypeArguments[0].Name})parser.Parse(action.{property.Name}[i]).Expression;
    }}
}}");
                    }
                    else
                    {
                        propertySb.AppendLine(
                            $@"action.{property.Name} = ({property.PropertyType.Name})parser.Parse(action.{property.Name}).Expression;");
                    }
                }
                var name = $"IqlTo{type.Name.Replace("Expression", "")}Parser";
                declarationsSb.AppendLine(
                    $@"    public class {name} : IqlToIqlActionParserBase<{type.Name}>
    {{
        public override IqlExpression ToQueryStringTyped<TEntity>({type.Name} action, IqlToIqlParserContext parser)
        {{
            {propertySb.ToString()}
            return action;
        }}
    }}

");
                registrationsSb.AppendLine(
                    $@"Registry.Register(typeof({type.Name}), () => new {name}());");
            }

            var parsers = declarationsSb.ToString();
            var registrations = registrationsSb.ToString();
        }
    }
}
#endif