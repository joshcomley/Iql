using Brandless.AspNetCore.OData.Extensions.Configuration;
using Iql.Entities;
using Iql.Server;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
    public class PersonTypeMapIqlConfigurator : IIqlEntitySetConfigurator
    {
        public void Configure(IEntityConfigurationBuilder builder)
        {
            builder.EntityType<PersonTypeMap>().Key.CanWrite = true;
        }
    }

    public class PersonTypeMapConfigurator : IODataEntitySetConfigurator
    {
        public void Configure(ODataModelBuilder builder)
        {
            builder.EntityType<PersonTypeMap>()
                .HasKey(p => new { p.PersonId, p.TypeId });
            builder
                .EntityType<PersonTypeMap>()
                .HasRequired(
                    d => d.Person,
                    (map, person) => map.PersonId == person.Id,
                    p => p.Types)
                    .CascadeOnDelete(false);
            builder
                .EntityType<PersonTypeMap>()
                .HasRequired(
                    d => d.Type,
                    (map, type) => map.TypeId == type.Id,
                    p => p.PeopleMap)
                    .CascadeOnDelete(false);
        }
    }
}