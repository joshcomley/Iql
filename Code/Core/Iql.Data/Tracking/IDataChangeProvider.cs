using System.Collections.Generic;
using Iql.Data.Context;
using Iql.Data.Crud.Operations;
using Iql.Entities;

namespace Iql.Data.Tracking
{
    public interface IDataChangeProvider
    {
        List<IEntityCrudOperationBase> GetInserts(IDataContext dataContext, object[] entities = null);
        List<IUpdateEntityOperation> GetUpdates(IDataContext dataContext, object[] entities = null, IProperty[] properties = null);
        List<IEntityCrudOperationBase> GetDeletions(IDataContext dataContext, object[] entities = null);
    }
}