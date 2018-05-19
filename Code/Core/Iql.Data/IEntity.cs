using Iql.Queryable.Events;

namespace Iql.Queryable.Data.EntityConfiguration
{
    public interface IEntity
    {
        EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; }
        EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
        EventEmitter<ExistsChangeEvent> ExistsChanged { get; set; }
    }
}