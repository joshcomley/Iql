using Iql.Data.Configuration.Relationships;

namespace Iql.Data.Tracking.State
{
    public class CascadeDeletion
    {
        public IRelationship Relationship { get; }
        public object Source { get; }

        public CascadeDeletion(IRelationship relationship, object source)
        {
            Relationship = relationship;
            Source = source;
        }
    }
}