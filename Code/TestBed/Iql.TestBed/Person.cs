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
        public List<PersonJob> Jobs { get; set; }
    }
    public class PersonJob : IPersonJobKey
    {
        private string _id;
        public int JobId { get; set; }
        public Job Job { get; set; }
        public int PersonId { get; set; }
        public Person Person { get; set; }
        public string Description { get; set; }
    }

    public interface IPersonJobKey
    {
        int JobId { get; set; }
        int PersonId { get; set; }
    }

    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<PersonJob> People { get; set; }
    }
}