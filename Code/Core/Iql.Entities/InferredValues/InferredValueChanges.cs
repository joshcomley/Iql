namespace Iql.Entities.InferredValues
{
    public class InferredValueChanges
    {
        public bool Success { get; }
        public IEntityConfiguration EntityConfiguration => SourceProperty.EntityConfiguration;
        public InferredValueChange[] Changes { get; }
        public IProperty SourceProperty { get; }
        public InferredValueChanges(bool success, object oldEntity, object currentEntity, IProperty sourceProperty, InferredValueChange[] changes)
        {
            Success = success;
            OldEntity = oldEntity;
            CurrentEntity = currentEntity;
            SourceProperty = sourceProperty;
            Changes = changes ?? new InferredValueChange[] { };
        }

        public void ApplyChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            if (Success)
            {
                foreach (var change in Changes)
                {
                    change.ApplyChange(entity);
                }
            }
        }

        public void UndoChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            if (Success)
            {
                foreach (var change in Changes)
                {
                    change.UndoChange(entity);
                }
            }
        }

        public object CurrentEntity { get; set; }
        public object OldEntity { get; set; }
    }
}