using Iql.Data.Context;
using Iql.Data.Lists;
using Iql.Entities.SpecialTypes;

namespace Iql.Data.SpecialTypes
{
    public abstract class
        SpecialTypeManager<TDefinition, TMapType, TKey> : ISpecialTypeManager<TDefinition, TMapType, TKey>
        where TDefinition : SpecialTypeDefinition
        where TMapType : class
    {
        private DbSet<TMapType, TKey> _set;
        public IDataContext DataContext { get; set; }
        public TDefinition Definition { get; set; }

        public DbSet<TMapType, TKey> Set => _set = _set ?? new DbSet<TMapType, TKey>(
                                                       DataContext.EntityConfigurationContext,
                                                       () => DataContext.DataStore,
                                                       null,
                                                       DataContext);

        protected SpecialTypeManager(IDataContext dataContext, SpecialTypeDefinition definition)
        {
            DataContext = dataContext;
            Definition = (TDefinition)definition;
        }
    }
}