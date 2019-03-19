using Iql.Data.Events;
using Iql.Entities.Events;
using Iql.Events;
using Newtonsoft.Json;

namespace Iql.Data
{
    public interface IEntity
    {
        [JsonIgnore]
        EventEmitter<IPropertyChangeEvent> PropertyChanging { get; set; }
        [JsonIgnore]
        EventEmitter<IPropertyChangeEvent> PropertyChanged { get; set; }
        [JsonIgnore]
        EventEmitter<ExistsChangeEvent> ExistsChanged { get; set; }
    }
}