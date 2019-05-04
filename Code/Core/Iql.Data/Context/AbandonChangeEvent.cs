using Iql.Data.Crud.Operations;
using Iql.Data.Tracking.State;

namespace Iql.Data.Context
{
    public class AbandonChangeEvent
    {
        private IDataContext _dataContext;

        public AbandonChangeEvent(IEntityStateBase entityState, IPropertyState property)
        {
            EntityState = entityState;
            Property = property;
        }

        public IEntityStateBase EntityState { get; }
        public IPropertyState Property { get; }
        // ReSharper disable AccessToStaticMemberViaDerivedType
        public IDataContext DataContext =>
            _dataContext = _dataContext ?? DataContextInternal.FindDataContextForEntity(EntityState.Entity);
        // ReSharper restore AccessToStaticMemberViaDerivedType
    }
}