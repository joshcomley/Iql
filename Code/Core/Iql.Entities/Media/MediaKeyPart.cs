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
                var config = MediaKey.Property.EntityConfiguration;
                var currentConfig = config;
                var parts = Key.Split('/');
                var relationshipParts = new List<string>();
                foreach (var part in parts)
                {
                    var child = currentConfig.FindProperty(part);
                    if (!child.Kind.HasFlag(PropertyKind.Relationship))
                    {
                        break;
                    }

                    currentConfig = child.Relationship.OtherEnd.Property.EntityConfiguration;
                    relationshipParts.Add(child.Name);
                }
                return IqlPropertyPath.FromString(string.Join("/", relationshipParts), config);
            }

            return null;
        }
    }
}