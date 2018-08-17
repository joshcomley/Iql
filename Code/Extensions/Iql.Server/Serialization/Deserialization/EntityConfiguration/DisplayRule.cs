using System;
using Iql.Entities.Rules.Display;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class DisplayRule : RuleBase, IDisplayRule
    {
        public Func<object, bool> Run { get; }
        public DisplayRuleKind Kind { get; set; }
        public DisplayRuleAppliesToKind AppliesToKind { get; set; }
    }
}