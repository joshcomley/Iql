using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class Client : DbObject
    {
        public List<ClientCategoryPivot> Categories { get; set; }
        public double AverageSales { get; set; }
        public double AverageIncome { get; set; }
        public int Category { get; set; }
        public string Description { get; set; }
        public double Discount { get; set; }
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Person> People { get; set; }
        public List<Person> InferredPeople { get; set; }
        public List<Site> Sites { get; set; }

        [Required(ErrorMessage = "Please select a management type")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a management type")]
        public int TypeId { get; set; }

        public ClientType Type { get; set; }
        //public int SomePersonId { get; set; }
        //public Person SomePerson { get; set; }
    }
}
