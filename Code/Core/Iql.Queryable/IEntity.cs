using Iql.Queryable.Data.Crud.State;
using Iql.Queryable.Data.Validation;
using Iql.Queryable.Events;

namespace Iql.Queryable
{
    public interface IEntity
    {
        EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; }
        EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
        EventEmitter<ExistsChangeEvent> ExistsChanged { get; set; }
    }
}