
using Iql.DotNet.Http;
using Iql.OData.Data;
using Iql.OData.Queryable;
#if TypeScript
using Iql.JavaScript.QueryToJavaScript;
#else
using Iql.Queryable.Data;
using Iql.DotNet.Queryable;
#endif
using Iql.Parsing;
using Iql.Queryable;
using Iql.Queryable.Data.DataStores;
using Iql.Queryable.Data.DataStores.InMemory;
using Iql.Queryable.Data.EntityConfiguration;

namespace Iql.TestBed
{
    public class AppDbContext : DataContext
    {
        public AppDbContext(
            IDataStore dataStore = null,
            EvaluateContext evaluateContext = null)
            : base(dataStore ?? new ODataDataStore(
//#if TypeScript
//                       new JavaScriptQueryableAdapter()
//#else
//                       new DotNetQueryableAdapter()
//#endif
                   ), evaluateContext)
        {
            var oDataConfiguration = new ODataConfiguration();
            oDataConfiguration.ApiUriBase = @"http://localhost:47000/odata/";
            oDataConfiguration.HttpProvider = new DotNetHttpProvider();
            oDataConfiguration.RegisterEntitySet<Person>(nameof(People));
            RegisterConfiguration(oDataConfiguration);
        }

        public DbSet<Person, int> People { get; set; }

        public override void Configure(EntityConfigurationBuilder builder)
        {
            new Db();

            builder.DefineEntity<Person>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Title)
                .DefineProperty(p => p.Age)
                ;

            var config = new JavaScriptQueryConfiguration();
            config.RegisterSource(() => Db.People);
            RegisterConfiguration(config);

            People = new DbSet<Person, int>(builder, () => DataStore, null, this);
        }
    }
}