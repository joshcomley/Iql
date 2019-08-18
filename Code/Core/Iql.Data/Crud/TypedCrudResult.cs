using System;
using Iql.Data.Crud.Operations;
using Iql.Data.Crud.Operations.Results;

namespace Iql.Data.Crud
{
    public class TypedCrudResult : CrudResultBase
    {
        public TypedCrudResult(ICrudOperation operation, Type entityType, bool success, RequestStatus requestStatus = RequestStatus.Online)
            : base(operation, success, requestStatus)
        {
            EntityType = entityType;
        }

        public Type EntityType { get; set; }
    }
}