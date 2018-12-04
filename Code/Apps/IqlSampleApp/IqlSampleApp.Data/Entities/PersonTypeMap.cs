using System;
using Brandless.Data.Entities;

namespace IqlSampleApp.Data.Entities
{
    public class PersonTypeMap : IHasGuid, ICreatedDate
    {
        public string Notes { get; set; }
        public string Description { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public PersonType Type { get; set; }
        public int TypeId { get; set; }
        public Guid Guid { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}