using System;
using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public class CrudOperation : ICrudOperation
    {
        public CrudOperation(IqlOperationKind kind, IDataContext dataContext)
        {
            Kind = kind;
            DataContext = dataContext;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }
        public IqlOperationKind Kind { get; }
        public IDataContext DataContext { get; }
    }
}