using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Server.Serialization.Deserialization.EntityConfiguration
{
    public class File : FileBase
    {
        public IMediaKey MediaKey
        {
            get => MediaKeyInternal;
            set => MediaKeyInternal = value;
        }
    }
}