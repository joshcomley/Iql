using System;
using Iql.Data.Context;

namespace Iql.Data.Crud.Operations
{
    public interface ICrudOperation
    {
        Guid Id { get; }
        IqlOperationKind Kind { get; }
        IDataContext DataContext { get; }
    }
}