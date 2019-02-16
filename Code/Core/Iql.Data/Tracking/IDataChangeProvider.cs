using System.Collections.Generic;
using Iql.Data.Crud.Operations;
using Iql.Entities;

namespace Iql.Data.Tracking
{
    public interface IDataChangeProvider
    {
        List<IEntityCrudOperationBase> GetInserts(object[] entities = null);
        List<IUpdateEntityOperation> GetUpdates(object[] entities = null, IProperty[] properties = null);
        List<IEntityCrudOperationBase> GetDeletions(object[] entities = null);
    }
}