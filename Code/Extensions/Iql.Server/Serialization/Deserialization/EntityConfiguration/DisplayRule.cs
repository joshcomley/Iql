using Iql.Entities.Rules.Display;
using System;

namespace Iql.Server.Serialization
{
    public class DisplayRule : RuleBase, IDisplayRule
    {
        public Func<object, bool> Run { get; }
        public DisplayRuleKind Kind { get; set; }
        public DisplayRuleAppliesToKind AppliesToKind { get; set; }
    }
}