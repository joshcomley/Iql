using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Iql.Queryable;
using Iql.Queryable.Data;

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
            await TestValidation();
            //await TestPaging();
            //await TestCreatingEntityWithCollection();
            ////await db.People.ToList();
            ////var dataResult1 = await db.PersonTypes.OrderBy(p => p.Title)
            ////    .ExpandCollection(
            ////        p => p.People,
            ////        p => p.Expand(pp => pp.Type)
            ////    )
            ////    .ToListWithResponse();
            ////var dataResult = await db.PersonTypes.OrderBy(p => p.Title)
            ////    .ExpandCollection(
            ////        p => p.People,
            ////        p => p.Where(person => person.Title.Contains("a")).Take(1)
            ////        .Expand(p2 => p2.Type)
            ////        )
            ////    .ToListWithResponse();
            ////foreach (var item in dataResult.Data)
            ////{
            ////    Console.WriteLine($"- {item.Title}");
            ////    foreach (var person in item.People)
            ////    {
            ////        PrintPerson(person);
            ////    }
            ////}

            ////Console.WriteLine("WithKey result:");
            ////var paulina = await db.People.WithKey(2);
            ////Console.WriteLine($"{paulina.Title} - Type: {paulina.Type.Title}");

            //await TestPeople(db);

            //personTypeMap2.Notes = "Gotcha";
            //personTypeMap2.Type.Title = "Polish test";
            //marta.Category = PersonCategory.Conventional;
            //marta.Title += " - 2";
            //await db.SaveChanges();
            //personTypeMap.Notes = "";
            //await db.SaveChanges();
            //db.PersonTypesMap.Delete(personTypeMap);
            //await db.SaveChanges();
            //marta.TypeId = null;
            //await db.SaveChanges();
            //marta.Title = null;
            //await db.SaveChanges();
            //marta.TypeId = null;
            //await db.SaveChanges();
            //int a = 0;
            //var personJob = new PersonJob { JobId = 2, Description = "first"};
            //marta.Jobs.Add(personJob);
            //personJob.SetFieldValue("_id", "test");
            //db.People.Add(marta);
            //await db.SaveChanges();
            //personJob.Description = "second";
            //await db.SaveChanges();
            //Console.WriteLine("Marta's ID: " + marta.Id);

            //Console.WriteLine("Fetching Marta:");
            //var martaDb = await new AppDbContext().People.WithKey(marta.Id);
            //Console.WriteLine("Fetched Marta ID: " + martaDb.Id);

            //Console.WriteLine("Updating string on Marta:");
            //marta.Description = "I've been updated";
            //await db.SaveChanges();
            //var martaDbAfterUpdate = await new AppDbContext().People.WithKey(marta.Id);

            //Console.WriteLine("Updating collection on Marta:");
            //marta.Jobs = marta.Jobs ?? new List<PersonJob>();
            //personJob.Description = "third";
            //marta.Jobs.Add(new PersonJob { JobId = 1, PersonId = marta.Id });
            //await db.SaveChanges();
            //db.PersonJobs.Delete(personJob);
            ////marta.Jobs.Remove(personJob);
            //await db.SaveChanges();

            //Console.WriteLine("Deleting Marta:");
            //db.People.Delete(marta);
            //await db.SaveChanges();
            //var martaDbAfterDelete = await new AppDbContext().People.WithKey(marta.Id);
            //Console.WriteLine("Fetched Marta after delete: " + (martaDbAfterDelete != null ? martaDbAfterDelete.Id.ToString() : "Not found"));




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

        private static async Task TestPaging()
        {
            var db = new AppDbContext();
            var refreshConfig = new EntityDefaultQueryConfiguration();
            refreshConfig.ConfigureDefaultGetOperations(() => db.People.Expand(p => p.Type)
                .ExpandCollection(p => p.Types, q => q.Expand(t => t.Type)));
            refreshConfig.ConfigureDefaultGetOperations(() => db.ReportCategories.Expand(p => p.ReportTypes));
            db.RegisterConfiguration(refreshConfig);
            var people = await db.People.ToList();
            var person1 = people.First();
            await people.LoadNextPage();
            var person2 = people.First();
            await people.LoadPreviousPage();
            var person3 = people.First();
            if (person1 == person3 && person1 != person2)
            {
                int ax = 0;
            }
            else
            {
                int ax = 0;
            }
            var nextPage = await people.NewNextPageQuery().ToList();
            var nextPage2 = await nextPage.NewNextPageQuery().ToList();
            var previousPage = await nextPage2.NewPreviousPageQuery().ToList();
            var previousPage2 = await previousPage.NewPreviousPageQuery().ToList();
            var personTypes = await db.PersonTypes.ToList();
            if (person1 == person2)
            {
                int ax = 0;
            }
            else
            {
                int ax = 0;
            }
        }

        private static async Task TestCreatingEntityWithCollection()
        {
            var db = new AppDbContext();
            var reportType1 = new ReportType();
            reportType1.Name = "Report type 1";
            var reportType2 = new ReportType();
            reportType2.Name = "Report type 2";
            var paulina1 = new ReportCategory();
            paulina1.Name = "Some category";
            paulina1.ReportTypes = paulina1.ReportTypes ?? new List<ReportType>();
            paulina1.ReportTypes.Add(reportType1);
            paulina1.ReportTypes.Add(reportType2);
            db.ReportCategories.Add(paulina1);
            await db.SaveChanges();
            reportType2.Name = "Report type 2b";
            await db.SaveChanges();
            reportType1.Name = "Report type 1a";
            reportType2.Name = "Report type 2a";
            await db.SaveChanges();
            int a = 0;
        }

        private static async Task TestValidation()
        {
            var db = new AppDbContext();
            var person = new Person();
            person.Types = person.Types ?? new DbList<PersonTypeMap>();
            person.Types.Add(new PersonTypeMap());
            person.Loading = new PersonLoading();
            db.People.Add(person);
            var result = await db.SaveChanges();
            int a = 0;
        }

        private static async Task TestPeople(AppDbContext db)
        {
            var polish = await db.PersonTypes.Where(p => p.Title == "Polish").Single();
            Console.WriteLine("Inserting Marta with Type ID:");
            var marta = new Person();
            marta.TypeId = 1;
            marta.Title = "Marta 1";
            marta.Description = "Test";
            marta.Types = marta.Types ?? new List<PersonTypeMap>();
            var personTypeMap = new PersonTypeMap
            {
                TypeId = polish.Data.Id,
                Notes = "test 1212"
            };
            marta.Types.Add(personTypeMap);
            var marta2 = new Person();
            marta2.TypeId = 1;
            marta2.Title = "Marta 2";
            marta2.Description = "Test";
            marta2.Types = marta2.Types ?? new List<PersonTypeMap>();
            var personTypeMap2 = new PersonTypeMap
            {
                TypeId = polish.Data.Id,
                Notes = "test 232323"
            };
            marta2.Types.Add(personTypeMap2);
            db.People.Add(marta);
            db.People.Add(marta2);
            await db.SaveChanges();
        }

        private static void PrintPerson(Person person)
        {
            Console.WriteLine($"{person.Id}: {person.Title} - {person.TypeId} - {person.Description}");//: Type = {person.Type.Title}");
        }
    }
}