using Iql.Queryable.Data.EntityConfiguration.Relationships;

namespace Iql.Queryable.Data.Crud.State
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