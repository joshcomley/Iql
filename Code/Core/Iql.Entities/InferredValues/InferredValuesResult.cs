using System.Linq;

namespace Iql.Entities.InferredValues
{
    public class InferredValuesResult
    {
        public InferredValueChanges[] Results { get; }
        public object CurrentEntity { get;  }
        public object OldEntity { get;  }
        public bool Success => Results == null ? true : Results.All(_ => _.Success);
        public void ApplyChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            foreach (var change in Results)
            {
                change.ApplyChanges(entity);
            }
        }

        public void UndoChanges(object entity = null)
        {
            entity = entity ?? CurrentEntity;
            foreach (var change in Results)
            {
                change.UndoChanges(entity);
            }
        }

        public InferredValuesResult(object oldEntity, object currentEntity, InferredValueChanges[] results)
        {
            Results = results;
            CurrentEntity = currentEntity;
            OldEntity = oldEntity;
        }
    }
}