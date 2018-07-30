using Iql.Entities.Rules;
using Iql.Entities.Validation;
using System;

namespace Iql.Server.Serialization
{
    public class ValidationRule : RuleBase, IBinaryRule, IValidationRule
    {
        public Func<object, bool> Run { get; }
    }
}