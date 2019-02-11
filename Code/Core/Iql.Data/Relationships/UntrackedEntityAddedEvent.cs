namespace Iql.Data.Relationships
{
    public class UntrackedEntityAddedEvent
    {
        public object Entity { get; set; }

        public UntrackedEntityAddedEvent(object entity)
        {
            Entity = entity;
        }
    }
}