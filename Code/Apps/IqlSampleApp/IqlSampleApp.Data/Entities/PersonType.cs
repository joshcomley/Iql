using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    /// <summary>
    ///     Independent
    ///     Tower
    ///     Birdcage
    ///     ... rest added/managed in web app
    /// </summary>
    public class PersonType : DbObject
    {
        [Required(ErrorMessage = ":Please enter a title")]
        public string Title { get; set; }

        //[Required(ErrorMessage = ":Please enter a description")]
        //public string Description { get; set; }
        public List<Person> People { get; set; }
        public List<PersonTypeMap> PeopleMap { get; set; }
    }
}
