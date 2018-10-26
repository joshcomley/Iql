using System.ComponentModel.DataAnnotations;
using IqlSampleApp.Data.Entities.Bases;

namespace IqlSampleApp.Data.Entities
{
    public class Project : DbObject
    {
        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
