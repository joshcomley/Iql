using Iql.Queryable.Data.EntityConfiguration.Validation;

namespace Iql.Queryable.Data.EntityConfiguration.Rules.Display
{
    public interface IDisplayRule : IRule
    {
        DisplayRuleKind Kind { get; set; }
    }
}