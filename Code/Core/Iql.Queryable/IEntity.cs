using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;

namespace Iql.Queryable
{
    public interface IEntity
    {
        bool OnSaving();
        bool OnDeleting();
        EntityValidationResult ValidateEntity();
        EventEmitter<IPropertyChangeEvent> PropertyChanging { get; }
        EventEmitter<IPropertyChangeEvent> PropertyChanged { get; }
    }
}