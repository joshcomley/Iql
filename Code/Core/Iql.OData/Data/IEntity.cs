using Iql.Queryable.Data.Validation;

namespace Iql.OData.Data
{
    public interface IEntity
    {
        bool OnSaving();
        bool OnDeleting();
        EntityValidationResult ValidateEntity();
        ODataDataStore GetODataDataStore();
    }
}