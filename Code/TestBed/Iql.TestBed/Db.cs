using System.Collections.Generic;

namespace Iql.TestBed
{
    public class Db
    {
        public Db()
        {
            if (!Initialized)
            {
                Initialized = true;
                People = new List<Person>();
                People.Add(new Person {TypeId = 32, Id = 2, Title = "Cara"});
                People.Add(new Person {TypeId = 24, Id = 1, Title = "Paulina"});
                People.Add(new Person {TypeId = 31, Id = 3, Title = "Kiera"});
            }
        }

        public static List<Person> People { get; set; }
        private static bool Initialized { get; set; }
    }
}