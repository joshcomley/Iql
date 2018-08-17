using System;
using Iql.Entities.Rules;
using Iql.Entities.Validation;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class ValidationRule : RuleBase, IBinaryRule, IValidationRule
    {
        public Func<object, bool> Run { get; }
    }
}