using Iql.Entities;
using Iql.Entities.PropertyGroups.Files;

namespace Iql.Server.Serialization
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