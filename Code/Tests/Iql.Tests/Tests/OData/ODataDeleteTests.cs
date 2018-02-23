﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                var db = NewDb();
                var client = new HazClient { PersistenceKey = new Guid("e4a693fc-1041-4dd9-9f57-7097dd7053a3") };
                db.Clients.Add(client);
                client.Name = "New client 123";
                await db.SaveChanges();
                db.Clients.Delete(client);
                await db.SaveChanges();
                var request = log.Deletes.Pop().Single();
                Assert.AreEqual(@"http://localhost:58000/odata/Clients(0)", request.Uri);
                Assert.IsNull(request.Body);
            });
        }
    }
}