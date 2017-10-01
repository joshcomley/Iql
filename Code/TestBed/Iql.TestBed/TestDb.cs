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
            var dataResult = await db.PersonTypes.OrderBy(p => p.Title)
                .ExpandCollection(
                    p => p.People,
                    p => p.Where(person => person.Title.Contains("a")).Take(1)
                    .Expand(p2 => p2.Type)
                    )
                .ToListWithResponse();
            foreach (var item in dataResult.Data)
            {
                Console.WriteLine($"- {item.Title}");
                foreach (var person in item.People)
                {
                    PrintPerson(person);
                }
            }

            Console.WriteLine("WithKey result:");
            var paulina = await db.People.Expand(p => p.Type).WithKey(2);
            Console.WriteLine($"{paulina.Title} - Type: {paulina.Type.Title}");

            Console.WriteLine("Inserting Marta with Type ID:");
            var marta = new Person();
            marta.TypeId = 1;
            marta.Title = "Marta";
            db.People.Add(marta);
            await db.SaveChanges();
            Console.WriteLine("Marta's ID: " + marta.Id);

            Console.WriteLine("Fetching Marta:");
            var martaDb = await new AppDbContext().People.WithKey(marta.Id);
            Console.WriteLine("Fetched Marta ID: " + martaDb.Id);

            Console.WriteLine("Updating Marta:");
            marta.Description = "I've been updated";
            await db.SaveChanges();
            var martaDbAfterUpdate = await new AppDbContext().People.WithKey(marta.Id);

            Console.WriteLine("Deleting Marta:");
            db.People.Delete(marta);
            await db.SaveChanges();
            var martaDbAfterDelete = await new AppDbContext().People.WithKey(marta.Id);
            Console.WriteLine("Fetched Marta after delete: " + (martaDbAfterDelete != null ? martaDbAfterDelete.Id.ToString() : "Not found"));
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
            Console.WriteLine($"{person.Id}: {person.Title} - {person.TypeId} - {person.Description}");//: Type = {person.Type.Title}");
        }
    }
}