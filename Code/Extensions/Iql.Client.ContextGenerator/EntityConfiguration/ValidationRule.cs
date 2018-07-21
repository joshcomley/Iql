using System;
using System.Linq.Expressions;
using Iql.Entities.Rules.Display;
using Iql.Entities.Rules.Relationship;

namespace Iql.OData.TypeScript.Generator.EntityConfiguration
{
    public class Rule
    {
        public string Iql { get; set; }
        public string Message { get; set; }
        public string Key { get; set; }
    }
    public class ValidationRule : Rule
    {
    }
    public class DisplayRule : Rule
    {
        public DisplayRuleAppliesToKind AppliesToKind { get; set; }
        public DisplayRuleKind Kind { get; set; }
    }
    public class RelationshipFilterRule : Rule, IRelationshipRule
    {
        public LambdaExpression Expression { get; }
        public Func<object, LambdaExpression> Run { get; }
    }
}