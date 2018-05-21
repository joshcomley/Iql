namespace Iql.Entities.Rules.Display
{
    public interface IDisplayRule : IBinaryRule
    {
        DisplayRuleKind Kind { get; set; }
        DisplayRuleAppliesToKind AppliesToKind { get; set; }
    }
}