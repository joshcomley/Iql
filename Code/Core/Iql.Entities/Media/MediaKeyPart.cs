using System.Collections.Generic;

namespace Iql.Entities
{
    public class MediaKeyPart : IMediaKeyPart
    {
        public IMediaKey MediaKey { get; set; }
        public bool IsPropertyPath { get; set; }
        public string Key { get; set; }

        public IqlPropertyPath GetRelationshipPath()
        {
            if (IsPropertyPath)
            {
                var config = MediaKey.File.EntityConfiguration;
                var currentConfig = config;
                var parts = Key.Split('/');
                var relationshipParts = new List<string>();
                foreach (var part in parts)
                {
                    var child = currentConfig.FindProperty(part);
                    if (!child.Kind.HasFlag(IqlPropertyKind.Relationship))
                    {
                        break;
                    }

                    currentConfig = child.Relationship.OtherEnd.Property.EntityConfiguration;
                    relationshipParts.Add(((IMetadata) child).Name);
                }

                var path = string.Join("/", relationshipParts);
                return string.IsNullOrWhiteSpace(path) ? null : IqlPropertyPath.FromString(config.Builder, path, config.TypeMetadata);
            }
            return null;
        }
    }
}