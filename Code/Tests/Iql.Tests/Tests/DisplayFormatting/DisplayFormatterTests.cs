using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;

namespace Iql.Tests.Tests.DisplayFormatting
{
    [TestClass]
    public class DisplayFormatterTests:TestsBase
    {
        [TestMethod]
        public void TestDisplayFormatter()
        {
            var person = new Person();
            person.Id = 7;
            person.Title = "Some guy";
            var personFormatted = Db.EntityConfigurationContext.EntityType<Person>().GetDisplayFormatting().Get("Report").Format(person);
            Assert.AreEqual($"{person.Title} ({person.Id})", personFormatted);
        }
    }
}