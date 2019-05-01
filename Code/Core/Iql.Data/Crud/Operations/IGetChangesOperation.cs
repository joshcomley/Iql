using Iql.Data.Context;
using Iql.Entities;

namespace Iql.Data.Crud.Operations
{
    public interface IGetChangesOperation
    {
        IDataContext DataContext { get; }
        object[] Entities { get; }
        IProperty[] Properties { get; }
    }
}