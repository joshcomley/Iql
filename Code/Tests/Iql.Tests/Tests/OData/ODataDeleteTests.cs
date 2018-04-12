﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataDeleteTests : ODataTestsBase
    {
        [TestMethod]
        public async Task TestDeleteSingleEntity()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                var db = NewHazDb();
                var client = EntityHelper.NewHazClient();
                db.Clients.Add(client);
                client.Name = "New client 123";
                await db.SaveChangesAsync();
                db.Clients.Delete(client);
                await db.SaveChangesAsync();
                var request = log.Deletes.Pop().Single();
                Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
                Assert.IsNull(request.Body);
            });
        }

        //[TestMethod]
        //public async Task TestDeleteRelatedEntity()
        //{
        //    await RequestLog.LogSessionAsync(async log =>
        //    {
        //        AppDbContext.InMemoryDb.People.Add(new Person { Id = 7 });
        //        AppDbContext.InMemoryDb.PeopleTypes.Add(new PersonType { Id = 2 });
        //        AppDbContext.InMemoryDb.PeopleTypeMap.Add(new PersonTypeMap { PersonId = 7, TypeId = 2 });
        //        var db = NewDb();
        //        var person = db.ExamManagers
        //        var request = log.Deletes.Pop().Single();
        //        Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
        //        Assert.IsNull(request.Body);
        //    });
        //}
    }
}