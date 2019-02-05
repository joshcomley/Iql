using System.Linq;
using Iql.Conversion;
using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;
using Iql.Entities;
using Newtonsoft.Json;

namespace Iql.Data.Tracking
{
    public class RemoteKeyMap : IJsonSerializable
    {
        public IEntityStateBase State { get; set; }
        public IPropertyState[] OldPropertyValues { get; set; }

        public CompositeKey Key { get; set; }

        public RemoteKeyMap(IEntityStateBase state, CompositeKey key)
        {
            State = state;
            Key = key;
        }

        public string SerializeToJson()
        {
            return JsonConvert.SerializeObject(PrepareForJson());
        }

        public object PrepareForJson()
        {
            return new
            {
                State = State.PrepareForJson(),
                OldPropertyValues = OldPropertyValues?.Select(_ => _.PrepareForJson()),
                Key = Key.PrepareForJson()
            };
        }
    }
}