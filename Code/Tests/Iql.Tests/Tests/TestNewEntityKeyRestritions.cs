using System;
using Iql.Data.Exceptions;
using Iql.Entities.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class TestNewEntityKeyRestritions : TestsBase
    {
        [TestMethod]
        public void AddingAnEntityWithAKeyWhenTheKeyIsGeneratedRemotelyShouldThrowException()
        {
            var client = new Client();
            client.Id = 7;
            ShouldThrowException<AttemptingToAssignRemotelyGeneratedKeyException>(() =>
            {
                Db.Clients.Add(client);
            });
        }

        [TestMethod]
        public void AssigningAKeyToANewEntityWhenItIsGeneratedRemotelyShouldThrowException()
        {
            var client = new Client();
            Db.Clients.Add(client);
            ShouldThrowException<Exception>(() =>
            {
                client.Id = 7;
            });
        }
    }
}