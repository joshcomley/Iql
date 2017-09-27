using System.Collections.Generic;

namespace Iql.TestBed
{
    public class PersonType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Person> People { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public PersonType Type { get; set; }
        public int TypeId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}