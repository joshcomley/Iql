using System;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public class TypedCrudResult : CrudResultBase
    {
        public TypedCrudResult(Type entityType, bool success, RequestStatus requestStatus = RequestStatus.Online) : base(success, requestStatus)
        {
            EntityType = entityType;
        }

        public Type EntityType { get; set; }
    }
}