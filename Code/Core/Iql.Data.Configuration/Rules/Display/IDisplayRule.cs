namespace Iql.Data.Configuration.Rules.Display
{
    public interface IDisplayRule : IBinaryRule
    {
        DisplayRuleKind Kind { get; set; }
    }
}