using System;
using System.ComponentModel.DataAnnotations;
using Brandless.Data.Entities;

namespace Tunnel.App.Data.Entities
{
    public class PersonTypeMap : IHasGuid, ICreatedDate
    {
        [Required(ErrorMessage = ":Please enter a title")]
        public string Notes { get; set; }
        [Required(ErrorMessage = ":Please enter a description")]
        public string Description { get; set; }
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public PersonType Type { get; set; }
        public int TypeId { get; set; }
        public Guid Guid { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}