using System;

namespace Iql.Entities.InferredValues
{
    public interface IInferredValueContext
    {
        Type EntityType { get; }
        object OldEntityState { get; set; }
        object CurrentEntityState { get; set; }
    }
}