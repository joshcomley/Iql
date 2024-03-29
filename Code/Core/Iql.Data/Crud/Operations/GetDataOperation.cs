using System;
using System.Linq;
using Iql.Data.Context;
using Iql.Data.Crud.Operations.Results;
using Iql.Data.Operations;
using Iql.Queryable;

namespace Iql.Data.Crud.Operations
{
    public class GetDataOperation<T> : EntitySetCrudOperation<T>,
        IGetDataOperation
        where T : class
    {
        private global::Iql.Queryable.IQueryable<T> _queryable;
        public bool IsSingleResult { get; private set; }

        public GetDataOperation(
            global::Iql.Queryable.IQueryable<T> queryable,
            IDataContext dataContext,
            Type mappedFromType = null) : base(IqlOperationKind.Get,
            dataContext)
        {
            Queryable = queryable;
            MappedFromType = mappedFromType;
        }

        IQueryableBase IGetDataOperation.Queryable
        {
            get => Queryable;
            set => Queryable = (global::Iql.Queryable.IQueryable<T>)value;
        }

        public global::Iql.Queryable.IQueryable<T> Queryable
        {
            get => _queryable;
            set
            {
                _queryable = value;
                IsSingleResult =
                    _queryable != null && _queryable.Operations.Any(o => o is WithKeyOperation);
            }
        }

        public Type MappedFromType { get; }
    }

    public interface IGetDataOperation
    {
        IQueryableBase Queryable { get; set; }
        bool IsSingleResult { get; }
        Type MappedFromType { get; }
    }
}