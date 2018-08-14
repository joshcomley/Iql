using System;
using Iql.Data.Context;
using Iql.Data.DataStores;
using Iql.Data.Lists;
using Iql.Entities;
using Iql.Parsing;
using Iql.Tests.Tests.OData;

namespace Iql.Tests.Data.Context.Custom
{
    public class MyCustomReportsSet : DbSet<MyCustomReport, Guid>
    {
        public MyCustomReportsSet(EntityConfigurationBuilder entityConfigurationBuilder, Func<IDataStore> dataStoreGetter, EvaluateContext evaluateContext = null, IDataContext dataContext = null) : base(entityConfigurationBuilder, dataStoreGetter, evaluateContext, dataContext)
        { }
    }

}