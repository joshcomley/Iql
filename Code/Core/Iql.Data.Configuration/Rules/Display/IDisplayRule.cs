namespace Iql.Queryable.Data.EntityConfiguration.Rules.Display
{
    public interface IDisplayRule : IBinaryRule
    {
        DisplayRuleKind Kind { get; set; }
    }
}