namespace Iql.Data.Crud.Operations
{
    public interface IEntitySaveOperation
    {
        IPropertyState[] GetChangedProperties();
    }
}