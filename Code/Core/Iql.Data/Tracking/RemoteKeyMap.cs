using System.Linq;
using Iql.Conversion;
using Iql.Data.Crud.Operations;
using Iql.Entities;

namespace Iql.Data.Tracking
{
    public class RemoteKeyMap : IJsonSerializable
    {
        public object Entity { get; set; }
        public IPropertyState[] OldPropertyValues { get; set; }

        public CompositeKey Key { get; set; }

        public RemoteKeyMap(object entity, CompositeKey key)
        {
            Entity = entity;
            Key = key;
        }

        public string SerializeToJson()
        {
            return this.ToJson();
        }

        public object PrepareForJson()
        {
            return new
            {
                //State = Entity.PrepareForJson(),
                OldPropertyValues = OldPropertyValues?.Select(_ => _.PrepareForJson()),
                Key = Key.PrepareForJson()
            };
        }
    }
}