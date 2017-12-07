using System;
using System.Linq;
using System.Threading.Tasks;
using Iql.Tests.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iql.Tests.Tests
{
    [TestClass]
    public class OneToOneRelationshipTests : TestsBase
    {
        [TestMethod]
        public void OneToOneRelationshipsSetShouldPersistRelationshipsWhenTracked()
        {
            void Perform(OneToOneTestType oneToOneTestType,
                OneToOneTestOrder order)
            {
                OneToOneShouldPersistRelationshipsWhenTracked(false, false, false, order, oneToOneTestType);
                TestCleanUp();
                OneToOneShouldPersistRelationshipsWhenTracked(false, false, true, order, oneToOneTestType);
                TestCleanUp();
                OneToOneShouldPersistRelationshipsWhenTracked(false, true, false, order, oneToOneTestType);
                TestCleanUp();
                OneToOneShouldPersistRelationshipsWhenTracked(false, true, true, order, oneToOneTestType);
                TestCleanUp();

                OneToOneShouldPersistRelationshipsWhenTracked(true, false, false, order, oneToOneTestType);
                TestCleanUp();
                OneToOneShouldPersistRelationshipsWhenTracked(true, false, true, order, oneToOneTestType);
                TestCleanUp();
                OneToOneShouldPersistRelationshipsWhenTracked(true, true, false, order, oneToOneTestType);
                TestCleanUp();
                OneToOneShouldPersistRelationshipsWhenTracked(true, true, true, order, oneToOneTestType);
                TestCleanUp();
            }
            Perform(OneToOneTestType.Reference, OneToOneTestOrder.BeforeAdd);
            Perform(OneToOneTestType.Reference, OneToOneTestOrder.AfterAdd);
            Perform(OneToOneTestType.Key, OneToOneTestOrder.BeforeAdd);
            Perform(OneToOneTestType.Key, OneToOneTestOrder.AfterAdd);
        }

        [TestMethod]
        public async Task OneToOneShouldPersistRelationshipDeletionsWhenTracked()
        {
            var riskAssessment1 = new RiskAssessment();
            var riskAssessment2 = new RiskAssessment();
            riskAssessment1.Id = 7;
            riskAssessment2.Id = 8;
            riskAssessment1.SiteInspectionId = 62;
            Db.RiskAssessments.Add(riskAssessment1);
            Db.RiskAssessments.Add(riskAssessment2);
            await Db.SaveChanges();
            Assert.IsNull(riskAssessment1.SiteInspection);
            Assert.IsNull(riskAssessment2.SiteInspection);

            // Externally modify the database
            var dbRiskAssessment = AppDbContext.InMemoryDb.RiskAssessments.Single(r => r.SiteInspectionId == 62);
            dbRiskAssessment.SiteInspectionId = 61;
            AppDbContext.InMemoryDb.RiskAssessments.Add(
                new RiskAssessment
                {
                    Id = 9,
                    SiteInspectionId = 62
                });
            AppDbContext.InMemoryDb.SiteInspections.Add(
                new SiteInspection
                {
                    Id = 62
                });

            var siteInspection = await Db.SiteInspections.Expand(d => d.RiskAssessment).WithKey(62);
            Assert.AreEqual(siteInspection.RiskAssessment.Id, 9);
            Assert.AreEqual(siteInspection.RiskAssessment.SiteInspectionId, siteInspection.Id);
            Assert.AreEqual(siteInspection.RiskAssessment.SiteInspection, siteInspection);
            Assert.AreEqual(61, riskAssessment1.SiteInspectionId);
        }
        public enum OneToOneTestType
        {
            Reference,
            Key
        }
        public enum OneToOneTestOrder
        {
            BeforeAdd,
            AfterAdd
        }
        public void OneToOneShouldPersistRelationshipsWhenTracked(
            bool reverseAddOrder,
            bool setSourceIds,
            bool setTargetIds,
            OneToOneTestOrder order,
            OneToOneTestType type)
        {
            if (!setTargetIds && type == OneToOneTestType.Key)
            {
                return;
            }
            var riskAssessment1 = new RiskAssessment();
            var riskAssessment2 = new RiskAssessment();
            if (setSourceIds)
            {
                riskAssessment1.Id = 7;
                riskAssessment2.Id = 8;
            }
            var siteInspection1 = new SiteInspection();
            var siteInspection2 = new SiteInspection();
            if (setTargetIds)
            {
                siteInspection1.Id = 62;
                siteInspection2.Id = 63;
            }

            void SetRelationships()
            {
                switch (type)
                {
                    case OneToOneTestType.Reference:
                        riskAssessment1.SiteInspection = siteInspection1;
                        riskAssessment2.SiteInspection = siteInspection2;
                        break;
                    case OneToOneTestType.Key:
                        riskAssessment1.SiteInspectionId = siteInspection1.Id;
                        riskAssessment2.SiteInspectionId = siteInspection2.Id;
                        break;
                }
            }

            if (order == OneToOneTestOrder.BeforeAdd)
            {
                SetRelationships();
            }
            void AddInspections()
            {
                Db.SiteInspections.Add(siteInspection1);
                Db.SiteInspections.Add(siteInspection2);
            }

            if (!reverseAddOrder)
            {
                AddInspections();
            }
            Db.RiskAssessments.Add(riskAssessment1);
            Db.RiskAssessments.Add(riskAssessment2);
            if (reverseAddOrder)
            {
                AddInspections();
            }
            if (order == OneToOneTestOrder.AfterAdd)
            {
                SetRelationships();
            }
            Assert.AreEqual(siteInspection1, riskAssessment1.SiteInspection);
            Assert.AreEqual(siteInspection1.Id, riskAssessment1.SiteInspectionId);
            Assert.AreEqual(siteInspection2, riskAssessment2.SiteInspection);
            Assert.AreEqual(siteInspection2.Id, riskAssessment2.SiteInspectionId);
            Assert.AreEqual(siteInspection1.RiskAssessment, riskAssessment1);
            Assert.AreEqual(siteInspection2.RiskAssessment, riskAssessment2);
        }
    }
}