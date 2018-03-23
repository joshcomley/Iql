using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tunnel.App.Data.Entities;
using Tunnel.Sets;

namespace Iql.Tests.Tests.DataContext
{
    [TestClass]
    public class DataContextTests : TestsBase
    {
        [TestMethod]
        public void GetDbSetBySet()
        {
            var someSet = Db.GetDbSetBySet<PersonInspectionSet>();
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbSet()
        {
            var someSet = Db.GetDbSet<PersonInspection, int>();
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbSetByEntityType()
        {
            var someSet = Db.GetDbSetByEntityType(typeof(PersonInspection));
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbSetBySetType()
        {
            var someSet = Db.GetDbSetBySetType(typeof(PersonInspectionSet));
            Assert.AreEqual(Db.PersonInspections, someSet);
        }

        [TestMethod]
        public void GetDbQueryable()
        {
            var someSet = Db.GetDbQueryable<PersonInspection>();
            Assert.AreEqual(Db.PersonInspections, someSet);
        }


        [TestMethod]
        public void GetDbSetNameByEntity()
        {
            var someSet = Db.GetDbSetPropertyNameByEntityType(typeof(PersonInspection));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }

        [TestMethod]
        public void GetDbSetNameBySet()
        {
            var someSet = Db.GetDbSetPropertyNameBySetType(typeof(PersonInspectionSet));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }


        [TestMethod]
        public void GetDbSetNameByEntityType()
        {
            var someSet = Db.GetDbSetPropertyNameByEntityType(typeof(PersonInspection));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }

        [TestMethod]
        public void GetDbSetNameBySetType()
        {
            var someSet = Db.GetDbSetPropertyNameBySetType(typeof(PersonInspectionSet));
            Assert.AreEqual(nameof(Db.PersonInspections), someSet);
        }
    }
}