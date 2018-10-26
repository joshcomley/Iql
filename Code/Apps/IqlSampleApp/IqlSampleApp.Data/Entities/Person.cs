using System.Collections.Generic;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    /// <summary>
    ///     Scan a person tag
    ///     Choose a person category
    ///     Choose a scafoold type
    ///     Choose a person loading type
    /// </summary>
    public class Person : DbObject
    {
//        public Site Site { get; set; }
//        public int SiteId { get; set; }
        public string Key { get; set; }
        //public string Location { get; set; }

        //[Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        public string Description { get; set; }

        public PersonCategory Category { get; set; }

        public PersonType Type { get; set; }
        public int? TypeId { get; set; }

        public List<PersonTypeMap> Types { get; set; }

        public PersonLoading Loading { get; set; }
        public int? LoadingId { get; set; }

        public List<PersonReport> Reports { get; set; }

//        public List<PersonInspection> PersonInspections { get; set; }
        public Client Client { get; set; }

        public int? ClientId { get; set; }
    }
}
