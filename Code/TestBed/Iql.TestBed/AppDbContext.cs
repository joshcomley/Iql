
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
            oDataConfiguration.RegisterEntitySet<PersonType>(nameof(PersonTypes));
            oDataConfiguration.RegisterEntitySet<PersonJob>(nameof(PersonJobs));
            RegisterConfiguration(oDataConfiguration);
        }

        public DbSet<Person, int> People { get; set; }
        public DbSet<PersonType, int> PersonTypes { get; set; }
        public DbSet<PersonJob, IPersonJobKey> PersonJobs { get; set; }

        public override void Configure(EntityConfigurationBuilder builder)
        {
            new Db();

            builder
                .DefineEntity<PersonType>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Title)
                .DefineCollectionProperty(p => p.People);

            builder.DefineEntity<Person>()
                .HasKey(p => p.Id)
                .DefineProperty(p => p.Title)
                .DefineProperty(p => p.Description)
                .DefineProperty(p => p.TypeId)
                .DefineProperty(p => p.Type)
                .DefineCollectionProperty(p => p.Jobs)
                .HasOne(p => p.Type)
                .WithMany(p => p.People)
                .WithConstraint(person => person.TypeId, type => type.Id)
                ;

            builder.DefineEntity<Person>()
                .HasMany(p => p.Jobs);

            builder.DefineEntity<PersonJob>()
                .HasCompositeKey<IPersonJobKey>(p => p.JobId, p => p.PersonId);

            builder.DefineEntity<PersonJob>()
                .DefineProperty(p => p.Description)
                .DefineProperty(p => p.Job)
                .DefineProperty(p => p.JobId)
                .DefineProperty(p => p.Person)
                .DefineProperty(p => p.PersonId)
                .HasOne(p => p.Job)
                .WithMany(p => p.People)
                .WithConstraint(p => p.JobId, p => p.Id)
                ;

            builder.DefineEntity<PersonJob>()
                .HasOne(p => p.Person)
                .WithMany(p => p.Jobs)
                .WithConstraint(p => p.PersonId, p => p.Id)
                ;

            var config = new JavaScriptQueryConfiguration();
            config.RegisterSource(() => Db.People);
            RegisterConfiguration(config);

            People = new DbSet<Person, int>(builder, () => DataStore, null, this);
        }
    }
}