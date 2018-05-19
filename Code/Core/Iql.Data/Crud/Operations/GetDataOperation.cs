using System.Linq;
using Iql.Queryable.Data.Context;
using Iql.Queryable.Operations;

namespace Iql.Queryable.Data.Crud.Operations
{
    public class GetDataOperation<T> : EntitySetCrudOperation<T> where T : class
    {
        private Queryable.IQueryable<T> _queryable;
        public bool IsSingleResult { get; private set; }
        public GetDataOperation(Queryable.IQueryable<T> queryable, IDataContext dataContext) : base(OperationType.Get,
            dataContext)
        {
            Queryable = queryable;
        }

        public Queryable.IQueryable<T> Queryable
        {
            get => _queryable;
            set
            {
                _queryable = value;
                IsSingleResult = 
                    _queryable != null && _queryable.Operations.Any(o => o is WithKeyOperation);
            }
        }
    }
}