using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class ReportCategoryConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            builder.EntityType<ReportCategory>()
                .ConfigureProperty(p => p.Name, property => property.SetNullable(false));
        }
    }
}