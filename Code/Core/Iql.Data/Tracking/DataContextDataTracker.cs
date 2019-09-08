using Iql.Data.Context;
using Iql.Entities;

namespace Iql.Data.Tracking
{
    public class DataContextDataTracker : DataTracker
    {
        public DataContextDataTracker(
            IDataContext dataContext,
            DataTrackerKind kind,
            IEntityConfigurationBuilder entityConfigurationBuilder,
            string name,
            bool offline = false,
            bool silent = false) : base(kind, entityConfigurationBuilder, name, offline, silent)
        {
            DataContext = dataContext;
        }

        public IDataContext DataContext { get; }
    }
}