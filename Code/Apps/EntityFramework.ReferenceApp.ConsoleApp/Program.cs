using System;
using System.Linq;

namespace EntityFramework.ReferenceApp.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.People.Add(new Person
                {
                    Id = 1,
                    Name = "Josh",
                    Age = 27
                });
                db.SaveChanges();
                foreach (var person in db.People.ToList())
                {
                    Console.WriteLine($"{person.Name} - {person.Age}");
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}