using System.Collections.Generic;

namespace Iql.OData.TypeScript.Generator.ConsoleApp.Library
{
    public class Db
    {
        public Db()
        {
            if (!Initialized)
            {
                Initialized = true;
                People = new List<Person>();
                People.Add(new Person {Age = 32, Id = 2, Name = "Cara"});
                People.Add(new Person {Age = 24, Id = 1, Name = "Paulina"});
                People.Add(new Person {Age = 31, Id = 3, Name = "Kiera"});
            }
        }

        public static List<Person> People { get; set; }
        private static bool Initialized { get; set; }
    }
}