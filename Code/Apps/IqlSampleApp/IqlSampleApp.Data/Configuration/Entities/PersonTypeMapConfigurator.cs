using Brandless.AspNetCore.OData.Extensions.Configuration;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNet.OData.Builder;

namespace IqlSampleApp.Data.Configuration.Entities
{
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