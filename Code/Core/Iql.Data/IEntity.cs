using Iql.Data.Events;
using Iql.Entities.Events;

namespace Iql.Data
{
    public interface IEntity
    {
        EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; }
        EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
        EventEmitter<ExistsChangeEvent> ExistsChanged { get; set; }
    }
}