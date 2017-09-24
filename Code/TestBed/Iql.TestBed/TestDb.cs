using System;
using System.Threading.Tasks;

namespace Iql.TestBed
{
    public class TestDb
    {
        public static async Task Run()
        {
            // TODO: Implement MethodCallExpressionParser based on CallJavaScriptExpressionParserIql
            // TODO: Wrap expand features of JavaScriptQuery into base class
            // TODO: Create new DotNetQuery class inheriting from above base class that constructs an expression tree
            // TODO: DotNetQuery ToList<>() should apply the expression tree to the list of elements
            //var x = JavaScriptCodeExtractor.ExtractBody("function (p) { return p.Id; }");
            var db = new AppDbContext();
            var people = await db.People.OrderBy(p => p.Name).ToListWithResponse();
            foreach (var person in people.Data)
            {
                PrintPerson(person);
            }


            var paulina = await db.People.WithKey(1);
            PrintPerson(paulina);

            //cara.Data[0].Name = "Changed!";
            //await db.SaveChanges();
            //db.People.Add(new Person
            //{
            //    Id = 7,
            //    Age = 27,
            //    Name = "Josh"
            //});
            //db.People.Add(new Person
            //{
            //    Id = 8,
            //    Age = 42,
            //    Name = "Adrian"
            //});
            //db.People.Add(new Person
            //{
            //    Id = 6,
            //    Age = 24,
            //    Name = "Paulina"
            //});
            //var db2 = new AppDbContext();
            //db2.People.Add(new Person
            //{
            //    Id = 9,
            //    Name = "Rose",
            //    Age = 33
            //});

            //await db2.SaveChanges();
            ////await db.SaveChanges();
            //var data =
            //    await db2
            //        .People
            //        .OrderBy(p => p.Age)
            //        .ToList();
            //await db.SaveChanges();
            //if (data != null && data.Data != null)
            //{
            //    foreach (var person in data.Data)
            //    {
            //        Console.WriteLine($"{person.Name} - {person.Age}");
            //    }
            //}
        }

        private static void PrintPerson(Person person)
        {
            Console.WriteLine($"{person.Id}: {person.Name} - {person.Age}");
        }
    }
}