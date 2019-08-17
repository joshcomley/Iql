using Iql.Data.Context;
using Iql.Data.Tracking.State;

namespace Iql.Data.Events
{
    public interface IEntityEventBase
    {
        IDataContext DataContext { get; }
        IEntityStateBase Entity { get; }
    }
}