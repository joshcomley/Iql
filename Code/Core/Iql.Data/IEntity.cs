using Iql.Data.Configuration.Events;
using Iql.Data.Events;

namespace Iql.Data
{
    public interface IEntity
    {
        EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; }
        EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
        EventEmitter<ExistsChangeEvent> ExistsChanged { get; set; }
    }
}