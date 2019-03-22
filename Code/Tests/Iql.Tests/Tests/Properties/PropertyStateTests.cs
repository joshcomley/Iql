using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Iql.Data.Extensions;
using Iql.Entities;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IqlSampleApp.Data.Entities;

namespace Iql.Tests.Tests.Properties
{
    [TestClass]
    public class PropertyStateTests : TestsBase
    {
        [TestMethod]
        public async Task UnchangedLocalValueShouldMatchRemoteValue()
        {
            var person = new Person();
            Assert.AreEqual(false, person.IsComplete);
            Db.People.Add(person);
            var state = Db.GetEntityState(person);
            var propertyState = state.GetPropertyState(nameof(Person.IsComplete));
            Assert.AreEqual(false, propertyState.LocalValue);
            Assert.AreEqual(propertyState.RemoteValue, propertyState.LocalValue);
        }
    }
}