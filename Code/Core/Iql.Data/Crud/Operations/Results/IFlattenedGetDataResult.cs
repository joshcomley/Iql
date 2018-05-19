using System;
using System.Collections;
using System.Collections.Generic;

namespace Iql.Data.Crud.Operations.Results
{
    public interface IFlattenedGetDataResult
    {
        bool Success { get; set; }
        IList Root { get; set; }
        Type EntityType { get; }
        Dictionary<Type, IList> Data { get; set; }
    }
}