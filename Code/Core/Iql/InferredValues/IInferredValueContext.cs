﻿using System;
using Iql.Entities.Rules.Relationship;

namespace Iql.Entities.InferredValues
{
    public interface IInferredValueContext : IEntityType
    {
        bool IsInitialize { get; set; }
        object PreviousEntityState { get; set; }
        object CurrentEntityState { get; set; }
    }
}