using System;
using Iql.Entities.Rules.Relationship;

namespace Iql.Entities.InferredValues
{
    public interface IInferredValueContext : IEntityType
    {
        object OldEntityState { get; set; }
        object CurrentEntityState { get; set; }
    }
}