using System;

namespace Iql.Queryable.Data.Crud
{
    public class TypedCrudResult : CrudResultBase
    {
        public TypedCrudResult(Type entityType, bool success) : base(success)
        {
            EntityType = entityType;
        }

        public Type EntityType { get; set; }
    }
}