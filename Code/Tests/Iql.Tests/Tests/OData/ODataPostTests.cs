﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haz.App.Data.Entities;
using Iql.OData.Data;
using Iql.Tests.Context;
using Iql.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Iql.Tests.Tests.OData
{
    [TestClass]
    public class ODataPostTests : ODataTestsBase
    {
        [TestMethod]
        public async Task TestPostSingleEntity()
        {
            await RequestLog.LogSessionAsync(async log =>
            {
                Assert.AreEqual(0, log.Posts.Count);
                var db = NewDb();
                var client = new HazClient { PersistenceKey = new Guid("e4a693fc-1041-4dd9-9f57-7097dd7053a3") };
                db.Clients.Add(client);
                client.Name = "New client 123";
                await db.SaveChanges();
                var request = log.Posts.Pop().Single();
                var changes = db.DataStore.Tracking.GetUpdates();
                Assert.AreEqual(0, changes.Count);
                Assert.AreEqual("http://localhost:58000/odata/Clients", request.Uri);                
                Assert.AreEqual(@"{
  ""Users"": [],
  ""Id"": 0,
  ""TypeId"": 0,
  ""Name"": ""New client 123"",
  ""Guid"": ""00000000-0000-0000-0000-000000000000"",
  ""CreatedDate"": ""0001-01-01T00:00:00.0+00:00"",
  ""Version"": 0,
  ""PersistenceKey"": ""e4a693fc-1041-4dd9-9f57-7097dd7053a3"",
  ""Videos"": [],
  ""Exams"": [],
  ""ExamManagers"": [],
  ""ExamResults"": [],
  ""ExamCandidateResults"": [],
  ""ExamCandidates"": [],
  ""Hazards"": []
}".CompressJson(), request.Body.Body.CompressJson());
                await db.SaveChanges();
                Assert.AreEqual(0, log.Posts.Count);
            });
        }
    }
}